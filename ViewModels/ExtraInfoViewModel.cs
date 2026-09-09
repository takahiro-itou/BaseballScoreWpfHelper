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
    this.m_selectIndex  = 1;
    this.m_dtRestGames  = new MatrixInfo();
    this.m_dtMagicInfo  = new MatrixInfo();
    this.m_dtWinsTable  = new MatrixInfo();
    this.m_currentInfo  = this.m_dtRestGames;
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
CurrentInfo  {
    get { return  this.m_currentInfo; }
    set {
        this.m_currentInfo = value;
        raisePropertyChanged();
    }
}

//----------------------------------------------------------------
/**
**
**/
public  virtual  MatrixInfo
MagicTable  {
    get { return  this.m_dtMagicInfo; }
}

//----------------------------------------------------------------
/**
**
**/
public  virtual  MatrixInfo
RestGameTable  {
    get { return  this.m_dtRestGames; }
}

//----------------------------------------------------------------
/**
**
**/
public  virtual  int
SelectedShowType  {
    get { return  this.m_selectIndex; }
    set {
        if ( this.m_selectIndex != value ) {
            this.m_selectIndex = value;
            raisePropertyChanged();
            updateCurrentInfo();
        }
    }
}

//----------------------------------------------------------------
/**
**
**/
public  virtual  MatrixInfo
WinsTable  {
    get { return  this.m_dtWinsTable; }
}


//========================================================================
//
//    For Internal Use Only.
//

//----------------------------------------------------------------
/**
**
**/

private  void  updateCurrentInfo()
{
    this.CurrentInfo = SelectedShowType switch
    {
        1 => m_dtRestGames,
        2 => m_dtMagicInfo,
        3 => m_dtWinsTable,
        _ => m_dtRestGames
    };
}


//========================================================================
//
//    Member Variables.
//

private   int           m_selectIndex;

private   MatrixInfo    m_currentInfo;

private   MatrixInfo    m_dtRestGames;

private   MatrixInfo    m_dtMagicInfo;

private   MatrixInfo    m_dtWinsTable;


}   //  End class  ExtraInfoViewModel

}   //  End of namespace  BaseballScoreHelper.ViewModels
