#pragma once
#include <ObjIdl.h>
#include <gdiplus.h>
class WindowImage
{
public:
	WindowImage();
	~WindowImage();

	void SetToCenter(RECT WindowRect);


private:
	Bitmap m_bitmap;
};

WindowImage::WindowImage()
{
}

WindowImage::~WindowImage()
{
}