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

using   BaseballScoreHelper.Services;
using   BaseballScoreHelper.Models;

using   WpfHelper.Commands;
using   WpfHelper.ViewModels;

using   System.Collections.ObjectModel;
using   System.Windows.Input;

using   LeagueInfo  = Score4Wrapper.Common.LeagueInfo;


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
        IWindowService  windowService,
        ScoreDocument   scoreDocument)
{
    this.m_windowService  = windowService;
    this.m_scoreDocument  = scoreDocument;

    m_windowCaption = "成績／順位";

    //  内部のビューモデルを構築。  //
    this.m_vmRanking = new RankingViewModel  (scoreDocument);
    this.m_vmExtras  = new ExtraInfoViewModel(scoreDocument);

    //  コマンドを実装する。      //
    this.FileOpenCommand    = new SimpleCommand(
        () => executeFileOpenCommand()
    );
    this.FileSaveCommand    = new SimpleCommand(
        () => executeFileSaveCommand(),
        _  => this.IsEnabled
    );
    this.FileSaveAsCommand  = new SimpleCommand(
        () => executeFileSaveAsCommand()
    );
    this.MagicLineCommand   = new SimpleCommand(
        () => executeMagicLineCommand(),
        _  => this.IsEnabled
    );
}


//========================================================================
//
//    Properties.
//

public  virtual  ICommand  FileOpenCommand { get; }

public  virtual  ICommand  FileSaveCommand { get; }

public  virtual  ICommand  FileSaveAsCommand { get; }

public  virtual  ICommand  MagicLineCommand { get; }


public  virtual  ExtraInfoViewModel
ExtraSource  {
    get { return  this.m_vmExtras; }
}

public  virtual  System.Boolean
IsEnabled  {
    get { return  this.m_isEnabled; }
    set {
        if ( this.m_isEnabled != value ) {
            this.m_isEnabled = value;
            raisePropertyChanged();
        }
    }
}


//----------------------------------------------------------------
/**
**
**/

public  virtual  ObservableCollection<LeagueInfo>
Leagues {
    get { return  this.m_scoreDocument.Leagues; }
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
protected  override  void
checkCommandsCanExecute(
        System.String?  propertyName)
{
    raiseCanExecuteChanged(FileSaveCommand);
    raiseCanExecuteChanged(this.MagicLineCommand);
}


//----------------------------------------------------------------
/**
**
**/

protected  virtual  void
executeFileOpenCommand()
{
    OpenFileDialogSettings  settings = new OpenFileDialogSettings {
        DefaultExt = ".gsr",
        FileName = "*.gsr",
        Filter = "Game Score Record(*.gsr)|*.gsr|All Files(*.*)|*.*",
        FilterIndex = 1
    };

    this.IsEnabled  = false;
    if ( m_windowService.showOpenFileDialog(settings) is string filePath )
    {
        this.IsEnabled  = this.m_scoreDocument.openBinaryData(filePath);
        raisePropertyChanged(nameof(Leagues));
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
    SaveFileDialogSettings  settings = new SaveFileDialogSettings {
        DefaultExt = ".gsr",
        FileName = "*.gsr",
        Filter = "Game Score Record(*.gsr)|*.gsr|All Files(*.*)|*.*",
        FilterIndex = 1
    };
    if ( m_windowService.showSaveFileDialog(settings) is string filePath )
    {
    }
}

//----------------------------------------------------------------
/**
**
**/
protected  virtual  void
executeMagicLineCommand()
{
    VictoryLineViewModel    vm  =
            new VictoryLineViewModel(this.m_scoreDocument);
    this.m_windowService.showLineView(vm);
}


//========================================================================
//
//    Member Variables.
//

private   readonly  IWindowService          m_windowService;

private   readonly  ScoreDocument           m_scoreDocument;

private   readonly  RankingViewModel        m_vmRanking;

private   readonly  ExtraInfoViewModel      m_vmExtras;


private   System.Boolean                    m_isEnabled;

private   System.String                     m_windowCaption;


}   //  End of class  MainViewModel

}   //  End of namespace  BaseballScoreHelper.ViewModels
