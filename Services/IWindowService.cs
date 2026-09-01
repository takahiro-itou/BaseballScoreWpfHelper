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
/**
**
**/
public  void
showLineView(VictoryLineViewModel viewModel);


}   //  End interface  IWindowService

}   //  End of namespace  BaseballScoreHelper.Services
