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

using System.ComponentModel;
using System.Runtime.CompilerServices;

using BaseballScoreHelper.Common;


namespace  BaseballScoreHelper.ViewModels  {

public  class  ViewModelBase : INotifyPropertyChanged
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
ViewModelBase()
{
}


//========================================================================
//
//    Properties.
//

//----------------------------------------------------------------
/**
**
**/
public  event PropertyChangedEventHandler?  PropertyChanged;


//========================================================================
//
//    Protected Member Functions.
//

//----------------------------------------------------------------
/**
**
**/
protected  virtual  INotifyCanExecuteChanged
getCommand(
        ICommand  command,
        [CallerArgumentExpression("command)"] string  paramName = "")
{
    if ( command is INotifyCanExecuteChanged raiseableCommand ) {
        return ( raiseableCommand );
    }

    throw new ArgumentException(
        $"指定されたコマンドは {nameof(INotifyCanExecuteChanged} を実装していません。プロパティ名: {paramName}",
        paramName);
}

//----------------------------------------------------------------
/**
**
**/
protected  virtual  void
raisePropertyChanged(
        [CallerMemberName]  System.String?  propertyName = null)
{
    PropertyChanged?.Invoke(
            this, new PropertyChangedEventArgs(propertyName));
}


}   //  End class  ViewModelBase

}   //  End of namespace  BaseballScoreHelper.ViewModels
