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

using System.Data;

using BaseballScoreHelper.Models;


namespace  BaseballScoreHelper.ViewModels  {

public  class  ExtraInfoViewModel : ViewModelBase
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
ExtraInfoViewModel()
{
    //  ダミーデータ。  //
    this.m_dtRestGames  = new DataTable();
    this.m_dtRestGames.Columns.Add("Team");
    this.m_dtRestGames.Columns.Add("Total");
    this.m_dtRestGames.Columns.Add("Team1");
    this.m_dtRestGames.Columns.Add("Team2");
    this.m_dtRestGames.Columns.Add("Team3");

    for ( int i = 1; i <= 3; ++ i ) {
        var row = this.m_dtRestGames.NewRow();
        row["Team"] = $"Team {i}";
        row["Total"] = 100;
        for ( int j = 1; j <= 3; ++ j ) {
            System.String   rowTeam = $"Team{j}";
            row[rowTeam] = 10 * i + j;
        }
        this.m_dtRestGames.Rows.Add(row);
    }
}


//========================================================================
//
//    Properties.
//

//----------------------------------------------------------------
/**
**
**/

public  virtual  DataTable
RestGameTable  {
    get { return  this.m_dtRestGames; }
}


//========================================================================
//
//    Member Variables.
//

private   DataTable     m_dtRestGames;


}   //  End class  ExtraInfoViewModel

}   //  End of namespace  BaseballScoreHelper.ViewModels
