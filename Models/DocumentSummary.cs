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
using   System.Windows.Media;

using   Wrapper         = Score4Wrapper;
using   WrapDocument    = Score4Wrapper.Document.ScoreDocument;

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

    this.m_dtRestGames  = new MatrixInfo();
    this.m_dtMagicInfo  = new MatrixInfo();
    this.m_dtWinsTable  = new MatrixInfo();

    //  ダミーデータ。  //
    this.m_dtRestGames  = new MatrixInfo();
    this.m_dtMagicInfo  = new MatrixInfo(4, 4);
    this.m_dtWinsTable  = new MatrixInfo(4, 4);

    this.m_dtMagicInfo.MatrixData[0].Value = "Teams";
    this.m_dtWinsTable.MatrixData[0].Value = "Teams";
    for ( int i = 1; i <= 3; ++ i ) {
        this.m_dtMagicInfo.MatrixData[i].Value      = $"Team {i}";
        this.m_dtMagicInfo.MatrixData[i*4].Value    = $"Team {i}";

        this.m_dtWinsTable.MatrixData[i].Value      = $"Team {i}";
        this.m_dtWinsTable.MatrixData[i*4].Value    = $"Team {i}";

        for ( int j = 1; j <= 3; ++ j ) {
            if ( i == j ) { continue; }
            this.m_dtMagicInfo.MatrixData[i*4+j].Value = $"{i * 3 + j}";
            this.m_dtMagicInfo.MatrixData[i*4+j].Background = Brushes.LightGreen;
            this.m_dtWinsTable.MatrixData[i*4+j].Value = $"{i*7} 勝/{i*10} 試合";
            this.m_dtWinsTable.MatrixData[i*4+j].Background = Brushes.Cyan;
        }
    }
}


//========================================================================
//
//    Public Member Functions.
//

//----------------------------------------------------------------
/**   集計済みデータから残り試合のテーブルを作成する。
**
**/
public  virtual  System.Boolean
buildRestGameTabel(
        WrapDocument    docScore,
        int             leagueIndex,
        int             scheduleFilter,
        int             gameType)
{
    int gameFilter;
    Wrapper.Common.CountedScores    scoreInfo;
    Wrapper.Common.TeamInfo         teamInfo;

    gameFilter = (scheduleFilter & (int)Wrapper.GameFilter.FILTER_SCHEDULE);
    gameFilter |= gameType;

    int numTeam = docScore.getNumTeams();
    int[]   bufShowIdx  = new int [numTeam];
    int numShow = docScore.computeRankOrder(leagueIndex, bufShowIdx);

    for ( int i = 0; i < numShow; ++ i ) {
        int idxTeam = bufShowIdx[i];
        teamInfo  = docScore.getTeamInfo(idxTeam);
        scoreInfo = docScore.getScoreInfo(idxTeam);
        writeTeamRestGamesToMatrixRow(
                this.m_dtRestGames, idxTeam,
                numTeam, numShow, bufShowIdx,
                gameFilter, scoreInfo
        );
    }

    return ( true );
}

//----------------------------------------------------------------
/**   集計済みデータから表示用の情報を生成する。
**
**/

public  virtual  void
generateViewInfoFromSummarizedDocument(
        WrapDocument    docScore,
        int             leagueIndex,
        int             magicMode)
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

//----------------------------------------------------------------
/**   プロパティ  MagicTable
**
**/
public  virtual  MatrixInfo
MagicTable  {
    get { return  this.m_dtMagicInfo; }
}

//----------------------------------------------------------------
/**   プロパティ  RankingData
**
**/
public  virtual  ObservableCollection<RankingModel>
RankingData {
    get { return  this.m_rankingData; }
}

//----------------------------------------------------------------
/**   プロパティ  RestGameTable
**
**/
public  virtual  MatrixInfo
RestGameTable  {
    get { return  this.m_dtRestGames; }
}

//----------------------------------------------------------------
/**   プロパティ WinsTable
**
**/
public  virtual  MatrixInfo
WinsTable  {
    get { return  this.m_dtWinsTable; }
}


//========================================================================
//
//    Protected Member Functions.
//

//========================================================================
//
//    For Internal Use Only.
//

private  void
writeTeamRestGamesToMatrixRow(
        MatrixInfo  destMatrix,
        int         idxRow,
        int         numTotalTeam,
        int         numLeagueTeam,
        int[]       showIndex,
        int         gameFilter,
        Wrapper.Common.CountedScores    scoreInfo)
{
    int restTotal, restLeague, restInter;
    int restVal;
    int trgTeam;

    int  colTotalAll    = 1;
    int  colLeagueTotal = numLeagueTeam + 2;
    int  colInterTotal  = numTotalTeam + 3;
    var  rowCells = destMatrix.MatrixData.AsSpan(
            idxRow * destMatrix.NumColumns, destMatrix.NumColumns);

    restTotal   = scoreInfo.NumTotalRestGames[gameFilter];
    restLeague  = scoreInfo.NumLeagueRestGames[gameFilter];
    restInter   = scoreInfo.NumInterRestGames[gameFilter];

    //  残り試合の合計  //
    rowCells[colTotalAll].Value = $"{restTotal}";
    rowCells[colTotalAll].Background = Brushes.Green;

    //  所属リーグ内の残り試合。対戦相手毎の試合数。    //
    for ( int j = 0; j < numLeagueTeam; ++ j ) {
    intn
        trgTeam = showIndex[j];
        restVal = scoreInfo.RestGames[trgTeam, gameFilter];
        rowCells[j + 2].Value = $"{restVal}";
    }
}

//========================================================================
//
//    Member Variables.
//

private   ObservableCollection<RankingModel>    m_rankingData;


private   MatrixInfo    m_dtRestGames;

private   MatrixInfo    m_dtMagicInfo;

private   MatrixInfo    m_dtWinsTable;


}   //  End of class  DocumentSummary

}   //  End of namespace  BaseballScoreHelper.Models
