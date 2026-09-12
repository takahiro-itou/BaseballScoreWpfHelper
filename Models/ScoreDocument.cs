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
//    ScoreDocument  class
//

public  class  ScoreDocument
{

//========================================================================
//
//    Constructor(s) and Destructor.
//

//----------------------------------------------------------------
/**   コンストラクタ。
**
**/
public  ScoreDocument()
{
    this.m_docScore     = new WrapDocument.ScoreDocument();
    this.m_leagueInfos  = new ObservableCollection<LeagueInfo>();
    this.m_rankingData  = new ObservableCollection<RankingModel>();
}


//========================================================================
//
//    Public Member Functions.
//

//----------------------------------------------------------------
/**
**
**/
public  virtual  System.Boolean
openBinaryData(
        System.String   fileName)
{
    DocumentFile.readFromBinaryFile(fileName, ref this.m_docScore);
    return  updateInfos();
}


//========================================================================
//
//    Properties.
//

public  virtual  ObservableCollection<LeagueInfo>
Leagues  {
    get { return  this.m_leagueInfos; }
}

public  virtual  ObservableCollection<RankingModel>
RankingData {
    get { return  this.m_rankingData; }
}


//========================================================================
//
//    Public Events.
//

public  event   Action?     LeagueInfoChanged;

public  event   Action?     RankingChanged;


//========================================================================
//
//    Protected Member Functions.
//

protected  virtual  void
generateScoreTable(
        int                     leagueIndex,
        Wrapper.MagicNumberMode magicMode)
{
    Wrapper.Common.TeamInfo         teamInfo;
    Wrapper.Common.CountedScores    scoreInfo;
    Wrapper.Common.MagicInfo        magicInfo;
    int     idxTeam, numTeam, numShow;
    int     numWons, numLost, numDraw;
    int     numGame, wpDenom;
    int     topDiff = 0;

    //  ダミーコード  今日の日付で処理する。  //
    System.DateTime currentDate = System.DateTime.Now;
    this.m_docScore.countScores(currentDate);

    const   int gameFilter  = (int)(Wrapper.GameFilter.FILTER_ALL_GAMES);

    numTeam = this.m_docScore.getNumTeams();
    int[]   bufShowIdx  = new int [numTeam];
    numShow = this.m_docScore.computeRankOrder(leagueIndex, bufShowIdx);

    this.m_rankingData = new ObservableCollection<RankingModel>();
    for ( int i = 0; i < numShow; ++ i ) {
        System.String    strDiff, strPerc, strMagic, strRank;

        idxTeam   = i;
        teamInfo  = this.m_docScore.getTeamInfo(idxTeam);
        scoreInfo = this.m_docScore.getScoreInfo(idxTeam);
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
            strDiff = $"{((topDiff - curDiff) / 2)}";
        }

        //  勝率。  //
        numGame = scoreInfo.NumGames[gameFilter];
        wpDenom = numGame - numDraw;
        if ( wpDenom == 0 ) {
            strPerc = "---";
        } else {
            strPerc = "${(numWons / wpDenom)}";
        }

        //  マジック。  /
        strMagic = "";

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

    return;
}


protected  virtual  void
notifyLeagueInfoChange()
{
    this.LeagueInfoChanged?.Invoke();
}

protected  virtual  void
notifyRankingChange()
{
    this.RankingChanged?.Invoke();
}


protected  virtual  System.Boolean
updateInfos()
{
    this.m_leagueInfos  = new ObservableCollection<LeagueInfo>();

    int numLeagues  = this.m_docScore.getNumLeagues();
    for ( int i = 0; i < numLeagues; ++ i ) {
        this.m_leagueInfos.Add(this.m_docScore.getLeagueInfo(i));
    }

    generateScoreData(0, Wrapper.MagicNumberMode.MAGIC_VICTORY);
    notifyRankingChange();
    notifyLeagueInfoChange();

    return ( true );
}


//========================================================================
//
//    For Internal Use Only.
//

//========================================================================
//
//    Member Variables.
//

private   WrapDocument.ScoreDocument            m_docScore;

private   ObservableCollection<LeagueInfo>      m_leagueInfos;

private   ObservableCollection<RankingModel>    m_rankingData;


}   //  End of class  ScoreDocument

}   //  End of namespace  BaseballScoreHelper.Models
