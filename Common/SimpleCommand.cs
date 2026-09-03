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
using System.Windows.Input;


namespace  BaseballScoreHelper.Common  {

//========================================================================
//
//    SimpleCommand  class.
//

public  class  SimpleCommand : AbstractSimpleCommand<Action>
{

//----------------------------------------------------------------
/**   コンストラクタ。
**
**/
public
SimpleCommand(
        Action              execute,
        Predicate<object?>? canExecute = null)
    : base(execute, canExecute)
{
}

//----------------------------------------------------------------
/**
**
**/
public  override  void
Execute(object? parameter)
{
    this.m_execute();
}

}   //  End class  SimpleCommand


//========================================================================
//
//    SimpleCommand<T>  class.
//

public  class  SimpleCommand<T> : AbstractSimpleCommand<Action<T> >
    where T : struct
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
SimpleCommand(
        Action<T>           execute,
        Predicate<object?>? canExecute = null)
    : base(execute, canExecute)
{
}


//========================================================================
//
//    Public Member Functions (Implement Interface).
//

//----------------------------------------------------------------
/**
**
**/
public  override  void
Execute(object? parameter)
{
    T tparam = default(T);
    if (parameter is not null) {
        tparam = (parameter is T)
            ? (T)parameter
            : convertFrom(parameter);
    }
    this.m_execute(tparam);
}

//========================================================================
//
//    Public Member Functions.
//

//----------------------------------------------------------------
/**
**
**/
public  static  T
convertFrom(object parameter)
{
    T?  tmp = (T?)s_typeConverter.ConvertFrom(parameter);
    if (tmp is T val) { return ( val ); }
    return  default(T);
}


//========================================================================
//
//    Member Variables.
//

private  static     TypeConverter
    s_typeConverter = TypeDescriptor.GetConverter(typeof(T));

}   //  End class  SimpleCommand<T>

}   //  End of namespace  BaseballScoreHelper.Common
