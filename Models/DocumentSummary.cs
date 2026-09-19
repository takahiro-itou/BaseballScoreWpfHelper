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


namespace  BaseballScoreHelper.Models  {

using   WrapScoreDocument   = WrapDocument::ScoreDocument;
using   CountedScores       = WrapCommon::CountedScores;
using   LeagueInfo          = WrapCommon::LeagueInfo;
using   TeamInfo            = WrapCommon::TeamInfo;
using   LeagueIndex         = System.Int32;
using   TeamIndex           = System.Int32;


//========================================================================
//
//    DocumentSummary  class
//

public  class  DocumentSummary
{

private   const  int    MAGIC_NO_PROBABILITY_WONS =
        (int)WrapNs.Consts.MAGIC_NO_PROBABILITY_WONS;

private   const  int    MAGICLIST_NO_DATA_ENTRY =
        (int)WrapNs.Consts.MAGICLIST_NO_DATA_ENTRY;


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
    this.m_flagSchedule = WrapNs.GameFilter.FILTER_SCHEDULE;

    //  ダミーデータ。  //
    this.m_dtRestGames  = new MatrixInfo();
    this.m_dtMagicInfo  = new MatrixInfo();
    this.m_dtWinsTable  = new MatrixInfo(4, 4);

    this.m_dtWinsTable.MatrixData[0].Value = "Teams";
    for ( int i = 1; i <= 3; ++ i ) {
        this.m_dtWinsTable.MatrixData[i].Value      = $"Team {i}";
        this.m_dtWinsTable.MatrixData[i*4].Value    = $"Team {i}";

        for ( int j = 1; j <= 3; ++ j ) {
            if ( i == j ) { continue; }
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
buildRestGameTable(
        WrapScoreDocument   docScore,
        int                 leagueIndex,
        WrapNs.GameFilter   scheduleFilter,
        WrapNs.GameFilter   gameType)
{
    CountedScores       scoreInfo;
    TeamInfo            teamInfo;
    WrapNs.GameFilter   gameFilter;

    gameFilter = (scheduleFilter & WrapNs.GameFilter.FILTER_SCHEDULE);
    gameFilter |= gameType;

    int numTeam = docScore.getNumTeams();
    int[]   bufShowIdx  = new int [numTeam];
    int numShow = docScore.computeRankOrder(leagueIndex, bufShowIdx);

    this.m_dtRestGames  = new MatrixInfo(numShow + 1, numTeam + 4);
    makeTeamListOnMatrixHeader(
            this.m_dtRestGames,
            numShow, bufShowIdx, numTeam, true, docScore);

    for ( int i = 0; i < numShow; ++ i ) {
        int idxTeam = bufShowIdx[i];
        teamInfo  = docScore.getTeamInfo(idxTeam);
        scoreInfo = docScore.getScoreInfo(idxTeam);
        writeTeamRestGamesToMatrixRow(
                this.m_dtRestGames,
                teamInfo.TeamName,
                i + 1,
                numTeam, numShow, bufShowIdx,
                gameFilter, scoreInfo
        );
    }

    return ( true );
}

//----------------------------------------------------------------
/**   集計済みデータからテーブルの内容を作成する。
**
**    対チームごとのマジックテーブル。
**/
public  virtual  System.Boolean
buildTeamMagicTable(
        WrapScoreDocument       docScore,
        LeagueIndex             leagueIndex,
        WrapNs.MagicNumberMode  magicMode)
{
    int     idxTeam, numTeam, numShow;

    numTeam = docScore.getNumTeams();
    int[]   bufShowIdx  = new int [numTeam];
    numShow = docScore.computeRankOrder(leagueIndex, bufShowIdx);

    this.m_dtMagicInfo  = new MatrixInfo(numShow + 1, numShow + 1);
    makeTeamListOnMatrixHeader(
            this.m_dtMagicInfo,
            numShow, bufShowIdx, numShow, false, docScore);

    for ( int i = 0; i < numShow; ++ i ) {
        idxTeam   = bufShowIdx[i];
        teamInfo  = docScore.getTeamInfo (idxTeam);
        scoreInfo = docScore.getScoreInfo(idxTeam);
    }

    return ( true );
}

//----------------------------------------------------------------
/**   集計済みデータからテーブルの内容を作成する。
**
**    各対戦相手毎に、その相手より上位になるために、
**  最低限勝利しなければならない試合数。
**/
public  virtual  System.Boolean
buildWinsForBeatTable(
        WrapScoreDocument       docScore,
        LeagueIndex             leagueIndex)
{
    return ( true );
}

//----------------------------------------------------------------
/**   集計済みデータから表示用の情報を生成する。
**
**/
public  virtual  void
generateViewInfoFromSummarizedDocument(
        WrapScoreDocument       docScore,
        LeagueIndex             leagueIndex,
        WrapNs.MagicNumberMode  magicMode)
{
    CountedScores        scoreInfo;
    TeamInfo             teamInfo;
    WrapCommon.MagicInfo magicInfo;

    int     idxTeam, numTeam, numShow;
    int     numWons, numLost, numDraw;
    int     numGame, wpDenom;
    decimal decVal  = 0;
    int     topDiff = 0;

    const  WrapNs.GameFilter
        gameFilter  = WrapNs.GameFilter.FILTER_ALL_GAMES;
    const  int  iGameFilter = (int)gameFilter;
    int         iMagicMode  = (int)magicMode;

    numTeam = docScore.getNumTeams();
    int[]   bufShowIdx  = new int [numTeam];
    numShow = docScore.computeRankOrder(leagueIndex, bufShowIdx);

    double[]  bufWinRates   = new double [numShow];
    int[]     bufShowDigits = new int [numShow];

    for ( int i = 0; i < numShow; ++ i ) {
        idxTeam = bufShowIdx[i];
        scoreInfo = docScore.getScoreInfo(idxTeam);
        numWons = scoreInfo.NumWons [iGameFilter];
        numLost = scoreInfo.NumLost [iGameFilter];
        numDraw = scoreInfo.NumDraw [iGameFilter];
        numGame = scoreInfo.NumGames[iGameFilter];
        wpDenom = numGame - numDraw;
        if ( wpDenom == 0 ) {
            bufWinRates[i]  = 0;
        } else {
            bufWinRates[i]  = (numWons * 1.0 / wpDenom);
       }
    }
    WrapDocument.ScoreDocument.makeDigitsList(
            bufWinRates, out bufShowDigits);

    this.m_rankingData = new ObservableCollection<RankingModel>();
    for ( int i = 0; i < numShow; ++ i ) {
        System.String    strDiff, strPerc, strMagic, strRank;

        idxTeam   = bufShowIdx[i];
        teamInfo  = docScore.getTeamInfo(idxTeam);
        scoreInfo = docScore.getScoreInfo(idxTeam);
        magicInfo = scoreInfo.TotalMagicInfo;

        numWons = scoreInfo.NumWons[iGameFilter];
        numLost = scoreInfo.NumLost[iGameFilter];
        numDraw = scoreInfo.NumDraw[iGameFilter];

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
        numGame = scoreInfo.NumGames[iGameFilter];
        wpDenom = numGame - numDraw;
        if ( wpDenom == 0 ) {
            strPerc = "---";
        } else {
            decVal  = (decimal)numWons / wpDenom;
            strPerc = decVal.ToString($"F{bufShowDigits[i]}");
        }

        //  マジック。  /
        strMagic = "";
        int magicValue  = magicInfo.MagicNumber[iMagicMode];
        if ( magicInfo.MagicFlags[iMagicMode] != 0 ) {
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

    this.buildRestGameTable(
            docScore, leagueIndex, this.m_flagSchedule, gameFilter);
    this.buildTeamMagicTable(
            docScore, leagueIndex, magicMode);
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

//----------------------------------------------------------------
/**
**
**/
private  void
makeTeamListOnMatrixHeader(
        MatrixInfo          destMatrix,
        TeamIndex           numShow,
        TeamIndex []        bufShowIndex,
        TeamIndex           numTeam,
        System.Boolean      flagShowTotal,
        WrapScoreDocument   docScore)
{
    int     col, idxTeam;

    if ( numTeam == -1 ) {
        numTeam = docScore.getNumTeams();
    }

    var  rowCells = destMatrix.MatrixData.AsSpan(0, destMatrix.NumColumns);

    //  左端にチーム名を表示する。  //
    col = 0;
    rowCells[col ++].Value  = "Team";

    if ( flagShowTotal ) {
        //  合計を表示する列。  //
        rowCells[col ++].Value  = "Total";
    }

    //  リーグ内のチーム。  //
    for ( int i = 0; i < numShow; ++ i ) {
        idxTeam = bufShowIndex[i];
        rowCells[col ++].Value  = docScore.getTeamInfo(idxTeam).TeamName;
    }

    if ( flagShowTotal ) {
        //  リーグの合計。  //
        rowCells[col ++].Value  = "League";
    }

    //  別リーグのチーム。  //
    for ( int i = numShow; i < numTeam; ++ i ) {
        idxTeam = bufShowIndex[i];
        rowCells[col ++].Value  = docScore.getTeamInfo(idxTeam).TeamName;
    }

    if ( flagShowTotal ) {
        //  別リーグの合計。    //
        rowCells[col ++].Value  = "Inter.";
    }

    return;
}

//----------------------------------------------------------------
/**
**
**/
private  void
writeTeamMagicToMatrixRow(
        Span<MatrixCellData>    refRow,
        System.String           teamName,
        TeamIndex               numTeam,
        TeamIndex               numShow,
        TeamIndex []            showIndex,
        TeamIndex               idxTeam,
        CountedScores           scoreInfo)
{
    for ( int j = 0; j < numShow; ++ j ) {
        TeamIndex  idxEnemy = shwoIndex[j];
        if ( idxTeam == idxEnemy ) {
            refRow[j + 1].Value = "--------";
            continue;
        }
    }

    return;
}

//----------------------------------------------------------------
/**
**
**/
private  void
writeTeamRestGamesToMatrixRow(
        MatrixInfo      destMatrix,
        System.String   teamName,
        int             idxRow,
        int             numTotalTeam,
        int             numLeagueTeam,
        int[]           showIndex,
        WrapNs.GameFilter           gameFilter,
        WrapCommon.CountedScores    scoreInfo)
{
    int restTotal, restLeague, restInter;
    int restVal;
    int trgTeam;
    int iGameFilter = (int)gameFilter;
    int col = 0;

    var  refRow = destMatrix.MatrixData.AsSpan(
            idxRow * destMatrix.NumColumns, destMatrix.NumColumns);

    restTotal   = scoreInfo.NumTotalRestGames [iGameFilter];
    restLeague  = scoreInfo.NumLeagueRestGames[iGameFilter];
    restInter   = scoreInfo.NumInterRestGames [iGameFilter];

    //  チーム名。      //
    refRow[col++].Value = teamName;

    //  残り試合の合計  //
    refRow[col].Background  = Brushes.Green;
    refRow[col++].Value     = $"{restTotal}";

    //  所属リーグ内の残り試合。対戦相手毎の試合数。    //
    for ( int j = 0; j < numLeagueTeam; ++ j ) {
        trgTeam = showIndex[j];
        restVal = scoreInfo.RestGames[trgTeam, iGameFilter];
        refRow[col++].Value = $"{restVal}";
    }

    refRow[col++].Value = $"{restLeague}";

    for ( int j = numLeagueTeam; j < numTotalTeam; ++ j ) {
        trgTeam = showIndex[j];
        restVal = scoreInfo.RestGames[trgTeam, iGameFilter];
        refRow[col++].Value = $"{restVal}";
    }

    refRow[col++].Value = $"{restInter}";

    return;
}

//========================================================================
//
//    Member Variables.
//

private   ObservableCollection<RankingModel>    m_rankingData;

private   MatrixInfo            m_dtRestGames;

private   MatrixInfo            m_dtMagicInfo;

private   MatrixInfo            m_dtWinsTable;

private   WrapNs.GameFilter     m_flagSchedule;


}   //  End of class  DocumentSummary

}   //  End of namespace  BaseballScoreHelper.Models
