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

using System.Windows.Input;


namespace  BaseballScoreHelper.Common  {

//========================================================================
//
//    INotifyCanExecuteChanged  interface.
//

public  interface  INotifyCanExecuteChanged : ICommand
{

//========================================================================
//
//    Public Events.


//========================================================================
//
//    Public Member Functions.
//

//----------------------------------------------------------------
/**   CanExecuteChanged イベントを発生させる。
**
**/

public  void
raiseCanExecuteChanged();


}   //  End class  AbstractSimpleCommand

}   //  End of namespace  WpfControl.Common
