//  -*-  coding: utf-8-with-signature-unix     -*-  //
/*************************************************************************
**                                                                      **
**                  ---  Baseball  Score  Project  ---                  **
**                                                                      **
**          Copyright (C), 2017-2026, Takahiro Itou                     **
**          All Rights Reserved.                                        **
**                                                                      **
**          License: (See COPYING or LICENSE files)                     **
**          GNU Affero General Public License (AGPL) version 3,         **
**          or (at your option) any later version.                      **
**                                                                      **
*************************************************************************/

using   System.Collections.ObjectModel;

using   Wrapper         = Score4Wrapper;
using   WrapDocument    = Score4Wrapper.Document;
using   DocumentFile    = Score4Wrapper.Document.DocumentFile;

using   LeagueInfo      = Score4Wrapper.Common.LeagueInfo;


namespace  BaseballScoreHelper.Models  {

//========================================================================
//
//    DocumentSummary  class
//

public  class  DocumentSummary
{

private   const  int    MAGIC_NO_PROBABILITY_WONS =
        (int)Wrapper.Consts.MAGIC_NO_PROBABILITY_WONS;

private   const  int    MAGICLIST_NO_DATA_ENTRY =
        (int)Wrapper.Consts.MAGICLIST_NO_DATA_ENTRY;

//========================================================================
//
//    Constructor(s) and Destructor.
//

//----------------------------------------------------------------
/**   コンストラクタ。
**
**/
public  DocumentSummary()
{
    this.m_rankingData  = new ObservableCollection<RankingModel>();
}


//========================================================================
//
//    Public Member Functions.
//

//----------------------------------------------------------------
/**   集計済みデータから表示用の情報を生成する。
**
**/

public  virtual  void
generateViewInfoFromSummarizedDocument(
        Wrapper.Document.ScoreDocument  docScore,
        int                             leagueIndex,
        int                             magicMode)
{
    Wrapper.Common.TeamInfo         teamInfo;
    Wrapper.Common.CountedScores    scoreInfo;
    Wrapper.Common.MagicInfo        magicInfo;

    int     idxTeam, numTeam, numShow;
    int     numWons, numLost, numDraw;
    int     numGame, wpDenom;
    decimal decVal  = 0;
    int     topDiff = 0;

    const   int gameFilter  = (int)(Wrapper.GameFilter.FILTER_ALL_GAMES);

    numTeam = docScore.getNumTeams();
    int[]   bufShowIdx  = new int [numTeam];
    numShow = docScore.computeRankOrder(leagueIndex, bufShowIdx);

    double[]  bufWinRates   = new double [numShow];
    int[]     bufShowDigits = new int [numShow];

    for ( int i = 0; i < numShow; ++ i ) {
        idxTeam = bufShowIdx[i];
        scoreInfo = docScore.getScoreInfo(idxTeam);
        numWons = scoreInfo.NumWons [gameFilter];
        numLost = scoreInfo.NumLost [gameFilter];
        numDraw = scoreInfo.NumDraw [gameFilter];
        numGame = scoreInfo.NumGames[gameFilter];
        wpDenom = numGame - numDraw;
        if ( wpDenom == 0 ) {
            bufWinRates[i]  = 0;
        } else {
            bufWinRates[i]  = (numWons * 1.0 / wpDenom);
       }
    }
    Wrapper.Document.ScoreDocument.makeDigitsList(
            bufWinRates, out bufShowDigits);

    this.m_rankingData = new ObservableCollection<RankingModel>();
    for ( int i = 0; i < numShow; ++ i ) {
        System.String    strDiff, strPerc, strMagic, strRank;

        idxTeam   = bufShowIdx[i];
        teamInfo  = docScore.getTeamInfo(idxTeam);
        scoreInfo = docScore.getScoreInfo(idxTeam);
        magicInfo = scoreInfo.TotalMagicInfo;

        numWons = scoreInfo.NumWons[gameFilter];
        numLost = scoreInfo.NumLost[gameFilter];
        numDraw = scoreInfo.NumDraw[gameFilter];

        //  ゲーム差。  //
        int curDiff = numWons - numLost;
        if ( i == 0 ) {
            topDiff = curDiff;
            strDiff = "---";
        } else if ( curDiff == topDiff ) {
            strDiff = "---";
        } else {
            decVal  = (decimal)(topDiff - curDiff) / 2;
            strDiff = decVal.ToString("F1");
        }

        //  勝率。  //
        numGame = scoreInfo.NumGames[gameFilter];
        wpDenom = numGame - numDraw;
        if ( wpDenom == 0 ) {
            strPerc = "---";
        } else {
            decVal  = (decimal)numWons / wpDenom;
            strPerc = decVal.ToString($"F{bufShowDigits[i]}");
        }

        //  マジック。  /
        strMagic = "";
        int magicValue  = magicInfo.MagicNumber[(int)(magicMode)];
        if ( magicInfo.MagicFlags[(int)(magicMode)] != 0 ) {
            if ( magicValue == MAGICLIST_NO_DATA_ENTRY ) {
                strMagic = "M --";
             } else {
                strMagic = $"M {magicValue}";
            }
        } else {
            if ( magicValue <= - MAGIC_NO_PROBABILITY_WONS ) {
                strMagic = "---";
            } else {
                strMagic = $"{magicValue}";
            }
        }

        //  確定順位範囲。  /
        if ( (magicInfo.RankHigh <= 0) && (magicInfo.RankLow <= 0) ) {
            strRank = "";
        } else if ( magicInfo.RankHigh == magicInfo.RankLow ) {
            strRank = $"{magicInfo.RankHigh}位確定";
        } else {
            strRank = $"{magicInfo.RankHigh}～{magicInfo.RankLow}";
        }

        //  所定の構造体にセットする。  //
        this.m_rankingData.Add(
            new  RankingModel {
                TeamName  = teamInfo.TeamName,
                NumGames  = numGame,
                NumWons   = numWons,
                NumLost   = numLost,
                NumDraw   = numDraw,
                GameDiff  = strDiff,
                Percent   = strPerc,
                MagicText = strMagic,
                RankRange = strRank
            }
        );
    }
}


//========================================================================
//
//    Properties.
//

public  virtual  ObservableCollection<RankingModel>
RankingData {
    get { return  this.m_rankingData; }
}


//========================================================================
//
//    Protected Member Functions.
//

//========================================================================
//
//    For Internal Use Only.
//

//========================================================================
//
//    Member Variables.
//

private   ObservableCollection<RankingModel>    m_rankingData;


}   //  End of class  DocumentSummary

}   //  End of namespace  BaseballScoreHelper.Models
