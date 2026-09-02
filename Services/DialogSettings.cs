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


namespace  BaseballScoreHelper.Services  {


//========================================================================
//
//    FileDialogSettingsBase  class.
//

public  class  FileDialogSettingsBase
{
    public  string  Title { get; set; } = string.Empty;

    public  string  Filter { get; set; } = "All Files(*.*)|*.*";

    public  string  InitialDirectory { get; set; } = string.Empty;
    public  string  DefaultExt { get; set; } = string.Empty;
    public  string  DefaultFileName { get; set; } = string.Empty;
};


//========================================================================
//
//    OpenFileDialogSettings  class.
//

public  class  OpenFileDialogSettings : FileDialogSettingsBase
{
    public  System.Boolean  MultiSelect { get; set; } = false;
}


//========================================================================
//
//    SaveFileDialogSettings  class.
//

public  class  SaveFileDialogSettings : FileDialogSettingsBase
{
    public  System.Boolean  OverwritePrompt { get; set; } = true;
}


}   //  End of namespace  BaseballScoreHelper.Services
