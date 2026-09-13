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
using   System.Windows.Media;


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
ExtraInfoViewModel(
        ScoreDocument   docScore)
{
    this.m_docScore = docScore;

    //  ダミーデータ。  //
    this.m_selectIndex  = 1;
    this.m_dtRestGames  = new MatrixInfo();
    this.m_dtMagicInfo  = new MatrixInfo(4, 4);
    this.m_dtWinsTable  = new MatrixInfo(4, 4);
    this.m_currentInfo  = this.m_dtRestGames;

    this.m_dtMagicInfo.MatrixData[0].Value = "Teams";
    this.m_dtWinsTable.MatrixData[0].Value = "Teams";
    for ( int i = 0; i < 3; ++ i ) {
        this.m_dtMagicInfo.MatrixData[i+1].Value     = $"Team {i}";
        this.m_dtMagicInfo.MatrixData[(i+1)*4].Value = $"Team {i}";

        this.m_dtWinsTable.MatrixData[i+1].Value     = $"Team {i}";
        this.m_dtWinsTable.MatrixData[(i+1)*4].Value = $"Team {i}";

        for ( int j = 0; j < 3; ++ j ) {
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
//    Properties.
//


//----------------------------------------------------------------
/**
**
**/
public  virtual  MatrixInfo
CurrentInfo  {
    get { return  this.m_currentInfo; }
    private set {
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
    this.CurrentInfo = this.SelectedShowType switch
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

private   readonly  ScoreDocument   m_docScore;

private   int           m_selectIndex;

private   MatrixInfo    m_currentInfo;

private   MatrixInfo    m_dtRestGames;

private   MatrixInfo    m_dtMagicInfo;

private   MatrixInfo    m_dtWinsTable;


}   //  End class  ExtraInfoViewModel

}   //  End of namespace  BaseballScoreHelper.ViewModels
