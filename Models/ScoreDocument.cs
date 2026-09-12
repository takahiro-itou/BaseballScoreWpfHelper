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
    this.m_scoreInfos   = new DocumentSummary[1, NUM_MAGIC_MODES];
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
SelectedMagicMode  {
    get { return  this.m_magicMode; }
    set {
        if ( this.m_magicMode != value ) {
            this.m_magicMode = value;
            notifyLeagueSummaryChanged();
        }
    }
}

public  virtual  int
SelectedLeagueIndex  {
    get { return  this.m_selectedLeague; }
    set {
        if ( this.m_selectedLeague != value ) {
            this.m_selectedLeague = value;
            notifySelectedLeagueChanged();
        }
    }
}

public  virtual  DocumentSummary
SelectedLeagueSummary  {
    get {
        return  this.m_scoreInfos[this.m_selectedLeague, this.m_magicMode];
    }
}


//========================================================================
//
//    Public Events.
//

public  event   Action?     LeagueListChanged;

public  event   Action?     SelectedLeagueChanged;

public  event   Action?     LeagueSummaryChanged;


//========================================================================
//
//    Protected Member Functions.
//

protected  virtual  void
generateScoreTables()
{
    int numLeagues  = this.m_docScore.getNumLeagues();
    for ( int i = 0; i < numLeagues; ++ i ) {
        this.m_scoreInfos[i, 0].generateViewInfoFromSummarizedDocument(
                this.m_docScore, i, 0);
        this.m_scoreInfos[i, 1].generateViewInfoFromSummarizedDocument(
                this.m_docScore, i, 1);
    }

    return;
}


protected  virtual  void
notifyLeagueListChanged()
{
    this.LeagueListChanged?.Invoke();
}


protected  virtual  void
notifyLeagueSummaryChanged()
{
    this.LeagueSummaryChanged?.Invoke();
}


protected  virtual  void
notifySelectedLeagueChanged()
{
    this.SelectedLeagueChanged?.Invoke();
    notifyLeagueSummaryChanged();
}


protected  virtual  System.Boolean
updateInfos()
{
    this.m_leagueInfos  = new ObservableCollection<LeagueInfo>();

    int numLeagues  = this.m_docScore.getNumLeagues();
    this.m_scoreInfos   = new DocumentSummary [numLeagues,2];

    for ( int i = 0; i < numLeagues; ++ i ) {
        for ( int m = 0; m < NUM_MAGIC_MODES; ++ m ) {
            this.m_scoreInfos[i, m] = new DocumentSummary();
        }
        this.m_leagueInfos.Add(this.m_docScore.getLeagueInfo(i));
    }

    summarizeDocument(System.DateTime.Now);
    notifyLeagueListChanged();
    notifySelectedLeagueChanged();
    notifyLeagueSummaryChanged();

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

private   int                                   m_magicMode;

private   DocumentSummary[,]                    m_scoreInfos;


}   //  End of class  ScoreDocument

}   //  End of namespace  BaseballScoreHelper.Models
