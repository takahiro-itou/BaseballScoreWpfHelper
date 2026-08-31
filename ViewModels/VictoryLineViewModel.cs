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

using System.Data;


namespace  BaseballScoreHelper.ViewModels  {

public  class  VictoryLineViewModel : ViewModelBase
{

//========================================================================
//
//    Constructor(s) and Destructor.
//

//----------------------------------------------------------------
/**   コンストラクタ。
**
**/

public
VictoryLineViewModel()
{
}


//========================================================================
//
//    Properties.
//

//----------------------------------------------------------------
/**
**
**/

public  virtual  DataTable
LineDataTable  {
    get { return  this.m_dtLines; }
}


//========================================================================
//
//    Member Variables.
//

private   DataTable     m_dtLines;


}   //  End class  VictoryLineViewModel

}   //  End of namespace  BaseballScoreHelper.ViewModels
