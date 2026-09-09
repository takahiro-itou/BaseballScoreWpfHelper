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

using   BaseballScoreHelper.Models;

using   WpfHelper.ViewModels;

using   System.Data;


namespace  BaseballScoreHelper.ViewModels  {

public  class  ExtraInfoViewModel : ViewModelBase
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
ExtraInfoViewModel()
{
    //  ダミーデータ。  //
    this.m_dtRestGames  = new MatrixInfo();
}


//========================================================================
//
//    Properties.
//

//----------------------------------------------------------------
/**
**
**/

public  virtual  MatrixInfo
RestGameTable  {
    get { return  this.m_dtRestGames; }
}


//========================================================================
//
//    Member Variables.
//

private   MatrixInfo    m_dtRestGames;


}   //  End class  ExtraInfoViewModel

}   //  End of namespace  BaseballScoreHelper.ViewModels
