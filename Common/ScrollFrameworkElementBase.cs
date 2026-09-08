//  -*-  coding: utf-8-with-signature  -*-  //
/*************************************************************************
**                                                                      **
**                  ---  WPF UserControl Library.  ---                  **
**                                                                      **
**          Copyright (C), 2026-2026, Takahiro Itou                     **
**          All Rights Reserved.                                        **
**                                                                      **
**          License: (See COPYING or LICENSE files)                     **
**          GNU Affero General Public License (AGPL) version 3,         **
**          or (at your option) any later version.                      **
**                                                                      **
*************************************************************************/

using   System.Windows.Controls;
using   System.Windows.Controls.Primitives;


namespace  WpfControl.Common  {


//========================================================================
//
//    AbstractScrollInfo  class.
//

public  abstract class  ScrollFrameworkElementBase
    : System.Windows.FrameworkElement, IScrollInfo
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
ScrollFrameworkElementBase()
{
}


//========================================================================
//
//    Public Member Functions (Implement Interface).
//

public  virtual  void
LineDown()
{
    SetVerticalOffset(this.VerticalOffset + this.SmallChangeY);
}

public  virtual  void
LineLeft()
{
    SetHorizontalOffset(this.HorizontalOffset - this.SmallChangeX);
}

public  virtual  void
LineRight()
{
    SetHorizontalOffset(this.HorizontalOffset + this.SmallChangeX);
}

public  virtual  void
LineUp()
{
    SetVerticalOffset(this.VerticalOffset - this.SmallChangeY);
}


public  virtual  System.Windows.Rect
MakeVisible(
        System.Windows.Media.Visual visual,
        System.Windows.Rect         rectangle)
{
    return ( rectangle );
}


public  virtual  void
MouseWheelDown()
{
    LineDown();
}

public  virtual  void
MouseWheelLeft()
{
    LineLeft();
}

public  virtual  void
MouseWheelRight()
{
    LineRight();
}

public  virtual  void
MouseWheelUp()
{
    LineUp();
}


public  virtual  void
PageDown()
{
    double  inc = getPageScrollAmount(ViewportHeight, SmallChangeY);
    SetVerticalOffset(this.VerticalOffset + inc);
}

public  virtual  void
PageLeft()
{
    double  inc = getPageScrollAmount(ViewportWidth, SmallChangeX);
    SetHorizontalOffset(this.HorizontalOffset - inc);
}

public  virtual  void
PageRight()
{
    double  inc = getPageScrollAmount(ViewportWidth, SmallChangeX);
    SetHorizontalOffset(this.HorizontalOffset + inc);
}

public  virtual  void
PageUp()
{
    double  inc = getPageScrollAmount(ViewportHeight, SmallChangeY);
    SetVerticalOffset(this.VerticalOffset - inc);
}


//----------------------------------------------------------------
/**
**
**/

public  virtual  void
SetHorizontalOffset(
        double  offset)
{
    double  val = Math.Max(0, Math.Min(
        offset, this.ExtentWidth - this.ViewportWidth));
    if ( this.m_scrollOffset.X != val ) {
        this.m_scrollOffset.X = val;
        invalidateScrollView();
    }
}

//----------------------------------------------------------------
/**
**
**/

public  virtual  void
SetVerticalOffset(
        double  offset)
{
    double  val = Math.Max(0, Math.Min(
        offset, this.ExtentHeight - this.ViewportHeight));
    if ( this.m_scrollOffset.Y != val ) {
        this.m_scrollOffset.Y = val;
        invalidateScrollView();
    }
}

//========================================================================
//
//    Properties (Abstract).
//

public  abstract  double  ExtentHeight { get; }

public  abstract  double  ExtentWidth  { get; }

public  abstract  double  SmallChangeX { get; }

public  abstract  double  SmallChangeY { get; }


//========================================================================
//
//    Properties (Implement Interface).
//

public  virtual  bool  CanHorizontallyScroll { get; set; }

public  virtual  bool  CanVerticallyScroll   { get; set; }


public  virtual  double  HorizontalOffset {
    get { return  this.m_scrollOffset.X; }
}


public  virtual  ScrollViewer?  ScrollOwner
{
    get { return  this.m_scrollOwner; }
    set {
        this.m_scrollOwner = value;
        this.m_scrollOwner?.InvalidateScrollInfo();
    }
}


public  virtual  double  VerticalOffset {
    get { return  this.m_scrollOffset.Y; }
}


public  virtual  double  ViewportHeight {
    get { return  this.m_viewport.Height; }
}

public  virtual  double  ViewportWidth {
    get { return  this.m_viewport.Width; }
}


//========================================================================
//
//    Protected Member Functions (Overrides).
//

//----------------------------------------------------------------
/**
**
**/

protected  override  System.Windows.Size
MeasureOverride(
        System.Windows.Size     availableSize)
{
    if ( this.m_viewport != availableSize ) {
        this.m_viewport = availableSize;
        this.m_scrollOwner?.InvalidateScrollInfo();
    }
    return ( availableSize );
}


//========================================================================
//
//    Protected Member Functions.
//

protected  virtual  double
getPageScrollAmount(
        double  vpSize,
        double  smSize)
{
    double  inc = vpSize - smSize;
    double  steps = smSize;
    double  scr = (steps > 0) ? Math.Floor(inc / steps) * steps : inc;
    return ( scr <= 0 ? vpSize : scr );
}


//----------------------------------------------------------------
/**
**
**/

protected  virtual  void
refreshViewport()
{
    this.InvalidateVisual();
}


//----------------------------------------------------------------
/**
**
**/

protected  virtual  void
invalidateScrollView()
{
    this.m_scrollOwner?.InvalidateScrollInfo();
    refreshViewport();
}


//========================================================================
//
//    Member Variables.
//

private   System.Windows.Size   m_viewport;

private   System.Windows.Point  m_scrollOffset;

private   ScrollViewer?         m_scrollOwner;

}   //  End class  AbstractScrollInfo

}   //  End of namespace  WpfControl.Common
