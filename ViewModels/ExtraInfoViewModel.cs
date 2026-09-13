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
ExtraInfoViewModel(
        ScoreDocument   docScore)
{
    this.m_docScore = docScore;

    docScore.LeagueListChanged      += OnLeagueListChanged;
    docScore.SelectedLeagueChanged  += OnSelectedLeagueChanged;
    docScore.LeagueSummaryChanged   += OnLeagueSummaryChanged;

    this.m_selectIndex  = 1;
    this.m_currentInfo  = docScore.SelectedLeagueSummary.RestGameTable;
}


//========================================================================
//
//    Properties.
//


//----------------------------------------------------------------
/**   プロパティ  CurrentInfo
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
/**   プロパティ  MagicTable
**
**/
public  virtual  MatrixInfo
MagicTable  {
    get { return  this.m_docScore.SelectedLeagueSummary.MagicTable; }
}

//----------------------------------------------------------------
/**
**
**/
public  virtual  MatrixInfo
RestGameTable  {
    get { return  this.m_docScore.SelectedLeagueSummary.RestGameTable; }
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
    get { return  this.m_docScore.SelectedLeagueSummary.WinsTable; }
}


//========================================================================
//
//    Event Handlers.
//

protected  virtual  void  OnLeagueListChanged()
{
}

protected  virtual  void  OnLeagueSummaryChanged()
{
    raisePropertyChanged(nameof(MagicTable));
    raisePropertyChanged(nameof(RestGameTable));
    raisePropertyChanged(nameof(WinsTable));
    raisePropertyChanged(nameof(CurrentInfo));
}

protected  virtual  void  OnSelectedLeagueChanged()
{
    OnLeagueSummaryChanged();
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
        1 => this.m_docScore.SelectedLeagueSummary.RestGameTable,
        2 => this.m_docScore.SelectedLeagueSummary.MagicTable,
        3 => this.m_docScore.SelectedLeagueSummary.WinsTable,
        _ => this.m_docScore.SelectedLeagueSummary.RestGameTable
    };
}


//========================================================================
//
//    Member Variables.
//

private   readonly  ScoreDocument   m_docScore;

private   int           m_selectIndex;

private   MatrixInfo    m_currentInfo;


}   //  End class  ExtraInfoViewModel

}   //  End of namespace  BaseballScoreHelper.ViewModels
