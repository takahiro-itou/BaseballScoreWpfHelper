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

using System.Collections.ObjectModel;

using BaseballScoreHelper.Models;


namespace  BaseballScoreHelper.ViewModels  {

public  class  RankingViewModel : ViewModelBase
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
RankingViewModel()
{
    this.m_rankingData  = new ObservableCollection<RankingModel>();
    this.m_rankingData.Add(
        new  RankingModel {
            TeamName  = "Team 1",
            NumWons   = 10,
            NumLost   = 3,
            NumDraw   = 1,
            NumGames  = 14,
            GameDiff  = "---",
            Percent   = ".769",
            MagicText = "9",
            RankRange = "1-6"
        }
    );
    this.m_rankingData.Add(
        new  RankingModel {
            TeamName  = "Team 2",
            NumWons   = 9,
            NumLost   = 3,
            NumDraw   = 2,
            NumGames  = 14,
            GameDiff  = "0.5",
            Percent   = ".750",
            MagicText = "8",
            RankRange = "1-6"
        }
    );
}


//========================================================================
//
//    Properties.
//

//----------------------------------------------------------------
/**
**
**/

public  virtual  ObservableCollection<RankingModel>
RankingData {
    get { return  this.m_rankingData; }
}


//========================================================================
//
//    Member Variables.
//

private   ObservableCollection<RankingModel>    m_rankingData;


}   //  End class  RankingViewModel

}   //  End of namespace  BaseballScoreHelper.ViewModels
