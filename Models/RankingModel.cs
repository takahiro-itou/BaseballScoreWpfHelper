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

public  class  RankingModel
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
RankingModel()
{
    this.TeamName   = "";
    this.NumWons    = 0;
    this.NumLost    = 0;
    this.NumDraw    = 0;
    this.Percent    = "";
    this.MagicText  = "";
    this.RankRange  = "";
}


//========================================================================
//
//    Properties.
//

public  System.String   TeamName  { get; set; }

public  int             NumWons   { get; set; }

public  int             NumLost   { get; set; }

public  int             NumDraw   { get; set; }

public  int             NumGames  { get; set; }

public  System.String   Percent   { get; set; }

public  System.String   MagicText { get; set; }

public  System.String   RankRange { get; set; }


//========================================================================
//
//    Member Variables.
//


}   //  End class  RankingModel

}   //  End of namespace  BaseballScoreHelper.Models
