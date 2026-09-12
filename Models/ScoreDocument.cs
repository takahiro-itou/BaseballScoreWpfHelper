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

private   const  int    NUM_MAGIC_MODES =
        (int)(Wrapper.MagicNumberMode.NUM_MAGIC_MODES);

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
    this.m_scoreInfos   = new DocumentSummary[1,2];
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

//----------------------------------------------------------------
/**   指定した日付までのデータを集計する。
**
**/
public  virtual  System.Boolean
summarizeDocument(
        System.DateTime   trgLastDate)
{
    //  データを集計する機能を呼び出す。    //
    this.m_docScore.countScores(trgLastDate);

    generateScoreTables();

    return ( true );
}


//========================================================================
//
//    Properties.
//

public  virtual  ObservableCollection<LeagueInfo>
Leagues  {
    get { return  this.m_leagueInfos; }
}


public  virtual  int
SelectedLeagueIndex  {
    get { return  this.m_selectedLeague; }
    set {
        if ( this.m_selectedLeague != value ) {
            this.m_selectedLeague = value;
            notifyRankingChange();
        }
    }
}

public  virtual  DocumentSummary
SelectedLeagueSummary  {
    get { return  this.m_scoreInfos[this.m_selectedLeague, 0]; }
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
generateScoreTables()
{
    int numLeagues  = this.m_docScore.getNumLeagues();
    for ( int i = 0; i < numLeagues; ++ i ) {
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
    this.m_scoreInfos   = new DocumentSummary [numLeagues,2];
    for ( int i = 0; i < numLeagues; ++ i ) {
        this.m_leagueInfos.Add(this.m_docScore.getLeagueInfo(i));
    }

    summarizeDocument(System.DateTime.Now);
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

private   int                                   m_selectedLeague;

private   DocumentSummary[,]                    m_scoreInfos;


}   //  End of class  ScoreDocument

}   //  End of namespace  BaseballScoreHelper.Models
