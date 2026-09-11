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
    return ( true );
}


//========================================================================
//
//    Properties.
//

public  virtual  ObservableCollection<LeagueInfo>
Leagues  {
    get { return  this.m_leagueInfos; }
    set { this.m_leagueInfos = value; }
}


protected  virtual  void
updateInfos()
{
    this.m_leagueInfos  = new ObservableCollection<LeagueInfo>();
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
