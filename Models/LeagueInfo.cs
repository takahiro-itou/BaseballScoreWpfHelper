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


namespace  BaseballScoreHelper.Models  {

public  class  LeagueInfo
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
LeagueInfo()
{
}


//========================================================================
//
//    Properties.
//

public  System.String   LeagueName { get; set; } = "";

public  int             NumPlayOff { get; set; } = 1;


}   //  End of class  LeagueInfo

}   //  End of namespace  BaseballScoreHelper.Models
