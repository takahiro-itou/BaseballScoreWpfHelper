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

using System.Collections.ObjectModel;
using System.Windows.Input;

using WpfControl.Common;
using BaseballScoreHelper.Document;
using BaseballScoreHelper.Services;


namespace  BaseballScoreHelper.ViewModels  {

public  class  MainViewModel : ViewModelBase
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
MainViewModel(
        IWindowService  windowService)
{
    this.m_windowService  = windowService;

    //  ダミーデータを準備する。    //
    this.m_leagueInfos = new ObservableCollection<LeagueInfo>();
    this.m_leagueInfos.Add(
        new LeagueInfo { LeagueName = "LeagueA", NumPlayOff = 3 });
    this.m_leagueInfos.Add(
        new LeagueInfo { LeagueName = "LeagueB", NumPlayOff = 3 });

    m_windowCaption = "成績／順位";

    //  内部のビューモデルを構築。  //
    this.m_vmRanking = new RankingViewModel();
    this.m_vmExtras  = new ExtraInfoViewModel();

    //  コマンドを実装する。      //
    this.FileOpenCommand = new SimpleCommand(
        () => executeFileOpenCommand()
    );
    this.FileSaveCommand = new SimpleCommand(
        () => executeFileSaveCommand()
    );
    this.FileSaveAsCommand = new SimpleCommand(
        () => executeFileSaveAsCommand()
    );
}


//========================================================================
//
//    Properties.
//

public  ICommand  FileOpenCommand { get; }

public  ICommand  FileSaveCommand { get; }

public  ICommand  FileSaveAsCommand { get; }


public  virtual  ExtraInfoViewModel
ExtraSource  {
    get { return  this.m_vmExtras; }
}

//----------------------------------------------------------------
/**
**
**/

public  virtual  ObservableCollection<LeagueInfo>
Leagues {
    get { return  this.m_leagueInfos; }
}

public  virtual  RankingViewModel
RankingSource  {
    get { return  this.m_vmRanking; }
}

//----------------------------------------------------------------
/**
**
**/

public  virtual  System.String
WindowCaption  {
    get { return  this.m_windowCaption; }
    set {
        this.m_windowCaption = value;
        raisePropertyChanged();
    }
}


//========================================================================
//
//    Protected Member Functions.
//

//----------------------------------------------------------------
/**
**
**/

protected  virtual  void
executeFileOpenCommand()
{
    Microsoft.Win32.OpenFileDialog  dlgOpenFile;

    dlgOpenFile = new Microsoft.Win32.OpenFileDialog {
        DefaultExt = ".gsr",
        FileName = "*.gsr",
        Filter = "Game Score Record(*.gsr)|*.gsr|All Files(*.*)|*.*",
        FilterIndex = 1
    };
    if ( dlgOpenFile.ShowDialog() == false ) {
        return;
    }
}

//----------------------------------------------------------------
/**
**
**/

protected  virtual  void
executeFileSaveCommand()
{
    WindowCaption = "上書き保存";
}

//----------------------------------------------------------------
/**
**
**/

protected  virtual  void
executeFileSaveAsCommand()
{
    Microsoft.Win32.SaveFileDialog  dlgSaveFile;

    dlgSaveFile = new Microsoft.Win32.SaveFileDialog {
        DefaultExt = ".gsr",
        FileName = "*.gsr",
        Filter = "Game Score Record(*.gsr)|*.gsr|All Files(*.*)|*.*",
        FilterIndex = 1
    };
    if ( dlgSaveFile.ShowDialog() == false ) {
        return;
    }
}


//========================================================================
//
//    Member Variables.
//

private   readonly  IWindowService          m_windowService;

private   readonly  RankingViewModel        m_vmRanking;

private   readonly  ExtraInfoViewModel      m_vmExtras;

private   System.String                     m_windowCaption;

private   ObservableCollection<LeagueInfo>  m_leagueInfos;


}   //  End class  MainViewModel

}   //  End of namespace  BaseballScoreHelper.ViewModels
