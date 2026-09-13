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
    this.m_dtLines  = new MatrixInfo(10 + 2, 4);
    for ( int r = 1; r <= 11; ++ r ) {
        int i = 11 - r;
        this.m_dtLines.MatrixData[r * 4 + 0].Value  = $"{i} 勝";
        if ( i >= 5 ) {
            this.m_dtLines.MatrixData[r * 4 + 1].Value  = "";
        } else {
            this.m_dtLines.MatrixData[r * 4 + 1].Value = $"{i}-{5-i}: {(i+5)/20}";
        }
        if ( i >= 6 ) {
            this.m_dtLines.MatrixData[r * 4 + 2].Value = "";
        } else {
            this.m_dtLines.MatrixData[r * 4 + 2].Value = $"{i}-{6-i}: {(i+4)/20}";
        }
        this.m_dtLines.MatrixData[r * 4 + 3].Value = $"{i}-{10-i}: {(i + 1)/20}";
    }

    this.m_currentInfo  = this.m_dtLines;
}


//========================================================================
//
//    Properties.
//

//----------------------------------------------------------------
/**
**
**/
public  virtual  MatrixInfo
CurrentInfo  {
    get { return  this.m_currentInfo; }
}

//----------------------------------------------------------------
/**
**
**/
public  virtual  ObservableCollection<LeagueInfo>
Leagues {
    get { return  this.m_docScore.Leagues; }
}


//----------------------------------------------------------------
/**
**
**/
public  virtual  MatrixInfo
LineDataTable  {
    get { return  this.m_dtLines; }
}


//========================================================================
//
//    Member Variables.
//

private   readonly  ScoreDocument   m_docScore;

private   MatrixInfo    m_currentInfo;

private   MatrixInfo    m_dtLines;


}   //  End of class  VictoryLineViewModel

}   //  End of namespace  BaseballScoreHelper.ViewModels
