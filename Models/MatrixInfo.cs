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

using   System.Windows.Media;


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
    this.m_cellData = new MatrixCellData[1];
    this.m_numCols  = 0;
    this.m_numRows  = 0;
    this.m_colSize  = new List<double>();
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
//    Public Member Functions.
//

//----------------------------------------------------------------
/**   指定した行範囲を取得する。
**
**/
public  virtual  Span<MatrixCellData>
getRowSpan(
        int  idxRow)
{
    return  this.m_cellData.AsSpan(idxRow * this.m_numCols, this.m_numCols);
}


//========================================================================
//
//    Properties.
//

public  virtual  List<double>  CustomHeights  {
    get { return  this.m_rowSize; }
    set { this.m_rowSize = value; }
}


public  virtual  List<double>  CustomWidths  {
    get { return  this.m_colSize; }
    set { this.m_colSize = value; }
}


public  double  DefaultHeight { get; set; } = 25.0;

public  double  DefaultWidth  { get; set; } = 60.0;


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
