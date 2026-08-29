//  -*-  coding: utf-8-with-signature  -*-  //
/*************************************************************************
**                                                                      **
**                  ---  WPF UserControl Library.  ---                  **
**                                                                      **
**          Copyright (C), 2026-2026, Takahiro Itou                     **
**          All Rights Reserved.                                        **
**                                                                      **
**          License: (See COPYING or LICENSE files)                     **
**          GNU Affero General Public License (AGPL) version 3,         **
**          or (at your option) any later version.                      **
**                                                                      **
*************************************************************************/

using System.ComponentModel;
using System.Windows.Input;

using ExecPred = System.Predicate<object?>;

namespace  WpfControl.Common  {

//========================================================================
//
//    AbstractSimpleCommand  class.
//

public abstract class  AbstractSimpleCommand<TDlgAct> : ICommand
    where TDlgAct : System.Delegate
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
AbstractSimpleCommand(
        TDlgAct     execute,
        ExecPred?   canExec = null)
{
    this.m_execute  = execute ?? throw new ArgumentNullException(
            nameof(execute));
    this.m_canExec  = canExec;
}


//========================================================================
//
//    Public Member Functions (Pure Virtual Functions).
//

//----------------------------------------------------------------
/**
**
**/
public  abstract  void
Execute(object? parameter);


//========================================================================
//
//    Public Member Functions (Implement Interface).
//

//----------------------------------------------------------------
/**   コマンドが実行可能か否かを返す。
**
**/
public  bool
CanExecute(object? parameter)
{
    return ( this.m_canExec?.Invoke(parameter) ?? true );
}


//========================================================================
//
//    Public Events (Implement Interface).
//

//----------------------------------------------------------------
/**
**
**/
public  event   EventHandler?   CanExecuteChanged;


//========================================================================
//
//    Public Member Functions.
//

//----------------------------------------------------------------
/**   CanExecuteChanged イベントを発生させる。
**
**/
public  virtual  void
raiseCanExecuteChanged()
{
    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}


//========================================================================
//
//    Member Variables.
//

/**   実行する内容。    **/
protected readonly  TDlgAct     m_execute;

/**   実行可否の判定。  **/
private   readonly  ExecPred?   m_canExec;


}   //  End class  AbstractSimpleCommand

}   //  End of namespace  WpfControl.Common
