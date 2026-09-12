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
RankingViewModel(
        ScoreDocument   docScore)
{
    this.m_docScore     = docScore;
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
    get { return  this.m_docScore.RankingData; }
}


//========================================================================
//
//    Member Variables.
//

private   readonly  ScoreDocument               m_docScore;


}   //  End of class  RankingViewModel

}   //  End of namespace  BaseballScoreHelper.ViewModels
