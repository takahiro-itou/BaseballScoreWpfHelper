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
    this.m_docScore = new WrapDocument.ScoreDocument();
    this.m_leagueInfos  = new ObservableCollection<LeagueInfo>();
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


//========================================================================
//
//    Protected Member Functions.
//

protected  virtual  void
generateScoreTable(
        int  leagueIndex,
        Wrapper.MagicNumberMode magicMode)
{
}

protected  virtual  System.Boolean
updateInfos()
{
    this.m_leagueInfos  = new ObservableCollection<LeagueInfo>();

    int numLeagues  = this.m_docScore.getNumLeagues();
    for ( int i = 0; i < numLeagues; ++ i ) {
        this.m_leagueInfos.Add(this.m_docScore.getLeagueInfo(i));
    }

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

private   WrapDocument.ScoreDocument        m_docScore;

private   ObservableCollection<LeagueInfo>  m_leagueInfos;


}   //  End of class  ScoreDocument

}   //  End of namespace  BaseballScoreHelper.Models
