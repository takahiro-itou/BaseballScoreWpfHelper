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


namespace  BaseballScoreHelper.ViewModels  {

using   LeagueInfo  = WrapCommon::LeagueInfo;


//========================================================================
//
//    MainViewModel  class
//

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

    scoreDocument.LeagueListChanged     += OnLeagueListChanged;
    scoreDocument.SelectedLeagueChanged += OnSelectedLeagueChanged;
    scoreDocument.LeagueSummaryChanged  += OnLeagueSummaryChanged;

    m_windowCaption = "成績／順位";

    //  内部のビューモデルを構築。  //
    this.m_vmExtras  = new ExtraInfoViewModel(scoreDocument);

    //  コマンドを実装する。      //
    this.FileOpenCommand    = new SimpleCommand(
        () => ExecuteFileOpenCommand()
    );
    this.FileSaveCommand    = new SimpleCommand(
        () => ExecuteFileSaveCommand(),
        _  => this.IsEnabled
    );
    this.FileSaveAsCommand  = new SimpleCommand(
        () => ExecuteFileSaveAsCommand()
    );
    this.MagicLineCommand   = new SimpleCommand(
        () => ExecuteMagicLineCommand(),
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


//----------------------------------------------------------------
/**   プロパティ  Leagues
**
**/
public  virtual  ExtraInfoViewModel
ExtraSource  {
    get { return  this.m_vmExtras; }
}

//----------------------------------------------------------------
/**   プロパティ  IsEnabled
**
**/
public  virtual  System.Boolean
IsEnabled  {
    get { return  this.m_isEnabled; }
    set { SetValue(ref this.m_isEnabled, value); }
}

//----------------------------------------------------------------
/**   プロパティ  Leagues
**
**/
public  virtual  ObservableCollection<LeagueInfo>
Leagues {
    get { return  this.m_scoreDocument.Leagues; }
}

//----------------------------------------------------------------
/**   プロパティ  SelectedDate
**
**/
public  virtual  System.DateTime?
SelectedDate  {
    get { return  this.m_scoreDocument.SelectedDate; }
    set {
        this.m_scoreDocument.SelectedDate = value;
        RaisePropertyChanged();
    }
}

//----------------------------------------------------------------
/**
**
**/
public  virtual  int
SelectedLeagueIndex {
    get { return  this.m_scoreDocument.SelectedLeagueIndex; }
    set { this.m_scoreDocument.SelectedLeagueIndex = value; }
}

//----------------------------------------------------------------
/**
**
**/
public  virtual  DocumentSummary
SelectedLeagueSummary {
    get { return  this.m_scoreDocument.SelectedLeagueSummary; }
}

//----------------------------------------------------------------
/**
**
**/
public  virtual  int
SelectedMagicMode  {
    get { return  this.m_scoreDocument.SelectedMagicMode; }
    set {
        this.m_scoreDocument.SelectedMagicMode = value;
        RaisePropertyChanged();
    }
}


//----------------------------------------------------------------
/**
**
**/

public  virtual  System.String
WindowCaption  {
    get { return  this.m_windowCaption; }
    set { SetValue(ref this.m_windowCaption, value); }
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
CheckCommandsCanExecute(
        System.String?  propertyName)
{
    RaiseCanExecuteChanged(FileSaveCommand);
    RaiseCanExecuteChanged(this.MagicLineCommand);
}


//----------------------------------------------------------------
/**
**
**/

protected  virtual  void
ExecuteFileOpenCommand()
{
    System.String   strCaption = "成績／順位　: ";

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
    } else {
        return;
    }

    System.DateTime  lastActiveDate = this.m_scoreDocument.LastActiveDate;
    System.DateTime  lastRecordDate = this.m_scoreDocument.LastRecordDate;

    strCaption += $"{lastActiveDate:yyyy/MM/dd}まで";
    strCaption += $" (日程は{lastRecordDate:yyyy/MM/dd}まで)";
    this.WindowCaption  = strCaption;
}


//----------------------------------------------------------------
/**
**
**/

protected  virtual  void
ExecuteFileSaveCommand()
{
    WindowCaption = "上書き保存";
}

//----------------------------------------------------------------
/**
**
**/

protected  virtual  void
ExecuteFileSaveAsCommand()
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
ExecuteMagicLineCommand()
{
    VictoryLineViewModel    vm  =
            new VictoryLineViewModel(this.m_scoreDocument);
    this.m_windowService.showLineView(vm);
}


//========================================================================
//
//    Event Handlers.
//

protected  virtual  void  OnLeagueListChanged()
{
    RaisePropertyChanged(nameof(Leagues));
}

protected  virtual  void  OnLeagueSummaryChanged()
{
    RaisePropertyChanged(nameof(SelectedLeagueSummary));
}

protected  virtual  void  OnSelectedLeagueChanged()
{
    RaisePropertyChanged(nameof(SelectedLeagueIndex));
    RaisePropertyChanged(nameof(SelectedLeagueSummary));
}



//========================================================================
//
//    Member Variables.
//

private   readonly  IWindowService          m_windowService;

private   readonly  ScoreDocument           m_scoreDocument;

private   readonly  ExtraInfoViewModel      m_vmExtras;


private   System.Boolean                    m_isEnabled;

private   System.String                     m_windowCaption;


}   //  End of class  MainViewModel

}   //  End of namespace  BaseballScoreHelper.ViewModels
