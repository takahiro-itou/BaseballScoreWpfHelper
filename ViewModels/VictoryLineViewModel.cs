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

using   BaseballScoreHelper.Models;

using   WpfHelper.ViewModels;

using   System.Collections.ObjectModel;
using   System.Data;

using   LeagueInfo  = Score4Wrapper.Common.LeagueInfo;


namespace  BaseballScoreHelper.ViewModels  {

public  class  VictoryLineViewModel : ViewModelBase
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
VictoryLineViewModel(
        ScoreDocument   docScore)
{
    this.m_docScore = docScore;

    //  ダミーデータ。  //
    System.String   keyHeadCol = "勝数";

    this.m_dtLines  = new DataTable();
    this.m_dtLines.Columns.Add(keyHeadCol);
    this.m_dtLines.Columns.Add("Team 1");
    this.m_dtLines.Columns.Add("Team 2");
    this.m_dtLines.Columns.Add("Team 3");

    for ( int i = 10; i >= 0; -- i ){
        var row = this.m_dtLines.NewRow();
        row[keyHeadCol] = i;
        if ( i >= 5 ) {
            row["Team 1"] = "";
        } else {
            row["Team 1"] = $"{i}-{5-i}: {(i + 5)/20}";
        }
        if ( i >= 6 ) {
            row["Team 2"] = "";
        } else {
            row["Team 2"] = $"{i}-{6-i}: {(i + 4)/20}";
        }
        row["Team 3"] = $"{i}-{10-i}: {(i + 1)/20}";
        this.m_dtLines.Rows.Add(row);
    }

    //  ダミーデータを準備する。    //
    this.m_leagueInfos = new ObservableCollection<LeagueInfo>();
    this.m_leagueInfos.Add(
        new LeagueInfo { leagueName = "LeagueA", numPlayOff = 3 });
    this.m_leagueInfos.Add(
        new LeagueInfo { leagueName = "LeagueB", numPlayOff = 3 });
}


//========================================================================
//
//    Properties.
//

//----------------------------------------------------------------
/**
**
**/

public  virtual  ObservableCollection<LeagueInfo>
Leagues {
    get { return  this.m_leagueInfos; }
}

//----------------------------------------------------------------
/**
**
**/

public  virtual  DataTable
LineDataTable  {
    get { return  this.m_dtLines; }
}


//========================================================================
//
//    Member Variables.
//

private   readonly  ScoreDocument           m_docScore;

private   DataTable     m_dtLines;

private   ObservableCollection<LeagueInfo>  m_leagueInfos;


}   //  End of class  VictoryLineViewModel

}   //  End of namespace  BaseballScoreHelper.ViewModels
