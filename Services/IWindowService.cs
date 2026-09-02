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

using BaseballScoreHelper.ViewModels;


namespace  BaseballScoreHelper.Services  {

public  interface  IWindowService  {


//----------------------------------------------------------------
/**   編集フォームを表示する。
**
**/
public  void
showEditForm(
        ScoreEditorViewModel    viewModel);

//----------------------------------------------------------------
/**   優勝ラインビューを表示する。
**
**/
public  void
showLineView(
        VictoryLineViewModel    viewModel);

//----------------------------------------------------------------
/**   ファイルを開くダイアログを表示する。
**
**/
public  System.String?
showOpenFileDialog(
        OpenFileDialogSettings  settings)
{
    Microsoft.Win32.OpenFileDialog  dlgOpenFile;

    dlgOpenFile = new Microsoft.Win32.OpenFileDialog {
        DefaultExt  = settings.DefaultExt,
        FileName    = settings.FileName,
        Filter      = settings.Filter,
        FilterIndex = settings.FilterIndex,
        InitialDirectory = settings.InitialDirectory,
        Title       = settings.Title,
        MultiSelect = false
    };

    if ( dlgOpenFile.ShowDialog() == false ) {
        return ( null );
    }
    return ( dlgOpenFile.FileName );
}


//----------------------------------------------------------------
/**   名前を付けて保存ダイアログを表示する。
**
**/
public  System.String?
showSaveFileDialog(
        SaveFileDialogSettings  settings)
{
    Microsoft.Win32.SaveFileDialog  dlgSaveFile;

    dlgSaveFile = new Microsoft.Win32.SaveFileDialog {
        DefaultExt  = settings.DefaultExt,
        FileName    = settings.FileName,
        Filter      = settings.Filter,
        FilterIndex = settings.FilterIndex,
        InitialDirectory = settings.InitialDirectory,
        OverwritePrompt = settings.OverwritePrompt,
        Title       = settings.Title
    };

    if ( dlgSaveFile.ShowDialog() == false ) {
        return ( null );
    }
    return ( dlgSaveFile.FileName );
}

}   //  End interface  IWindowService

}   //  End of namespace  BaseballScoreHelper.Services
