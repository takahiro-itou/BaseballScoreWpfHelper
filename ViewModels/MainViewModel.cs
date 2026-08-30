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

using BaseballScoreHelper.Document;


namespace  BaseballScoreHelper.ViewModels  {

public  class  MainViewModel : ViewModelBase
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
MainViewModel()
{

    //  ダミーデータを準備する。    //
    this.m_leagueInfos = new ObservableCollection<LeagueInfo>();
    this.m_leagueInfos.Add(
        new LeagueInfo { LeagueName = "LeagueA", NumPlayOff = 3 });
    this.m_leagueInfos.Add(
        new LeagueInfo { LeagueName = "LeagueB", NumPlayOff = 3 });
}


//========================================================================
//
//    Properties.
//

public  virtual  ObservableCollection<LeagueInfo>
Leagues {
    get { return  this.m_leagueInfos; }
}


//========================================================================
//
//    Member Variables.
//

private  ObservableCollection<LeagueInfo>   m_leagueInfos;


}   //  End class  MainViewModel

}   //  End of namespace  BaseballScoreHelper.ViewModels
