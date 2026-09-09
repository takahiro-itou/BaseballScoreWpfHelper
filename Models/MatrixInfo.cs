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

using   WpfControl.Editor;


namespace  BaseballScoreHelper.Models  {

//========================================================================
//
//    MatrixInfo  class
//

public  class  MatrixInfo
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
MatrixInfo()
{
    //  テスト用にダミーデータを用意。  //
    this.m_cellData = new MatrixCellData[4 * 5];
    this.m_numCols  = 4;
    this.m_numRows  = 5;
    this.m_colSize  = new List<double>() { 120 };
    this.m_rowSize  = new List<double>();
}

public
MatrixInfo(
    int     numRows,
    int     numCols)
{
    this.m_cellData = new MatrixCellData[numRows * numCols];
    this.m_numCols  = numCols;
    this.m_numRows  = numRows;
    this.m_colSize  = new List<double>();
    this.m_rowSize  = new List<double>();
}


//========================================================================
//
//    Properties.
//

public  virtual  List<double>  CustomHeights  {
    get { return  this.m_rowSize; }
}


public  virtual  List<double>  CustomWidths  {
    get { return  this.m_colSize; }
}


public  virtual  MatrixCellData[]  MatrixData {
    get { return  this.m_cellData; }
    set { this.m_cellData = value; }
}


public  virtual  int   NumColumns  {
    get { return  this.m_numCols; }
}

public  virtual  int   NumRows  {
    get { return  this.m_numRows; }
}


//========================================================================
//
//    Member Variables.
//

private   MatrixCellData[]      m_cellData;

private   int                   m_numCols;

private   int                   m_numRows;

private   List<double>          m_colSize;

private   List<double>          m_rowSize;


}   //  End of class  MatrixInfo

}   //  End of namespace  BaseballScoreHelper.Models
