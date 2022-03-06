// LW2.cpp : Defines the entry point for the application.
//

#include "framework.h"
#include "LW2.h"
#include <ObjIdl.h>
#include <gdiplus.h>
#include <stdexcept>
#include <memory>
#include <commdlg.h>

#define MAX_LOADSTRING 100

// Global Variables:
HINSTANCE hInst;                                // current instance
WCHAR szTitle[MAX_LOADSTRING];                  // The title bar text
WCHAR szWindowClass[MAX_LOADSTRING];            // the main window class name

// Forward declarations of functions included in this code module:
ATOM                MyRegisterClass(HINSTANCE hInstance);
BOOL                InitInstance(HINSTANCE, int);
LRESULT CALLBACK    WndProc(HWND, UINT, WPARAM, LPARAM);
INT_PTR CALLBACK    About(HWND, UINT, WPARAM, LPARAM);
int                 MainLoop();

HINSTANCE g_hInstance = NULL;

using namespace Gdiplus;
using namespace std;

auto_ptr<Bitmap> g_pBitmap;

class CGdiplusInitializer
{
public:
    CGdiplusInitializer()
    {
        Gdiplus::GdiplusStartupInput input;
        Gdiplus::GdiplusStartupOutput output;

        if (Gdiplus::GdiplusStartup(&m_token, &input, &output) != Gdiplus::Ok)
        {
            // Не удалось инициализировать GDI+
            throw std::runtime_error("Failed to initialize GDI+");
        }
    }

    ~CGdiplusInitializer()
    {
        Gdiplus::GdiplusShutdown(m_token);
    }
private:
    ULONG_PTR m_token;
};

int APIENTRY wWinMain(_In_ HINSTANCE hInstance,
                     _In_opt_ HINSTANCE hPrevInstance,
                     _In_ LPWSTR    lpCmdLine,
                     _In_ int       nCmdShow)
{
    UNREFERENCED_PARAMETER(hPrevInstance);
    UNREFERENCED_PARAMETER(lpCmdLine);

    CGdiplusInitializer initializer;

    // Initialize global strings
    LoadStringW(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
    LoadStringW(hInstance, IDC_LW2, szWindowClass, MAX_LOADSTRING);
    MyRegisterClass(hInstance);

    // Perform application initialization:
    if (!InitInstance (hInstance, nCmdShow))
    {
        return FALSE;
    }

    HACCEL hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_LW2));

    MSG msg;

    // Main message loop:
    int result = MainLoop();

    g_pBitmap.release();

    return result;
}



//
//  FUNCTION: MyRegisterClass()
//
//  PURPOSE: Registers the window class.
//
ATOM MyRegisterClass(HINSTANCE hInstance)
{
    WNDCLASSEXW wcex;

    wcex.cbSize = sizeof(WNDCLASSEX);

    wcex.style          = CS_HREDRAW | CS_VREDRAW;
    wcex.lpfnWndProc    = WndProc;
    wcex.cbClsExtra     = 0;
    wcex.cbWndExtra     = 0;
    wcex.hInstance      = hInstance;
    wcex.hIcon          = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_LW2));
    wcex.hCursor        = LoadCursor(nullptr, IDC_ARROW);
    wcex.hbrBackground  = (HBRUSH)(COLOR_WINDOW+1);
    wcex.lpszMenuName   = NULL;
    wcex.lpszClassName  = szWindowClass;
    wcex.hIconSm        = LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_SMALL));

    return RegisterClassExW(&wcex);
}

//
//   FUNCTION: InitInstance(HINSTANCE, int)
//
//   PURPOSE: Saves instance handle and creates main window
//
//   COMMENTS:
//
//        In this function, we save the instance handle in a global variable and
//        create and display the main program window.
//
BOOL InitInstance(HINSTANCE hInstance, int nCmdShow)
{
   hInst = hInstance; // Store instance handle in our global variable

   HMENU hMainMenu = LoadMenu(hInstance, MAKEINTRESOURCE(IDR_MAIN_MENU));

   HWND hWnd = CreateWindowW(szWindowClass, szTitle, WS_OVERLAPPEDWINDOW,
      CW_USEDEFAULT, 0, CW_USEDEFAULT, 0, nullptr, hMainMenu, hInstance, nullptr);

   if (!hWnd)
   {
      return FALSE;
   }

   ShowWindow(hWnd, nCmdShow);
   UpdateWindow(hWnd);

   return TRUE;
}

int MainLoop()
{

    HACCEL accel = LoadAccelerators(g_hInstance, MAKEINTRESOURCE(IDR_MAIN_MENU));

    MSG msg;
    BOOL res;
    while ((res = GetMessage(&msg, NULL, 0, 0)) != 0)
    {
        if (res == -1)
        {
            // произошла ошибка - нужно обработать ее и, вероятно,
            // завершить работу приложения
        }
        else
        {
            // Пытаемся обработать сообщение как сообщение от нажатия клавиш
            // быстрого доступа
            if (!TranslateAccelerator(msg.hwnd, accel, &msg))
            {
                // Это не сообщение о нажатии клавиш быстрого доступа
                // обрабатываем сообщение стандартным образом

                // Если это сообщение о нажатии виртуальной клавиши,
                // то добавляем в очередь сообщений сообщения, несущие информацию о
                // коде вводимого пользователем символа
                TranslateMessage(&msg);
                // передаем сообщение в соответствующую оконную процедуру
                DispatchMessage(&msg);
            }
        }
    }

    // сюда мы попадем только в том случае извлечения сообщения WM_QUIT
    // msg.wParam содержит код возврата, помещенный при помощи функции PostQuitMessage()
    return msg.wParam;
}

//
//  FUNCTION: WndProc(HWND, UINT, WPARAM, LPARAM)
//
//  PURPOSE: Processes messages for the main window.
//
//  WM_COMMAND  - process the application menu
//  WM_PAINT    - Paint the main window
//  WM_DESTROY  - post a quit message and return
//
//


void OnPaint(HWND hwnd);

void OnDestroy(HWND);

void OnCommand(HWND hwnd, int id, HWND hwndCtl, UINT codeNotify);

LRESULT CALLBACK WndProc(HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam)
{
    switch (message)
    {
        HANDLE_MSG(hwnd, WM_DESTROY, OnDestroy);
        HANDLE_MSG(hwnd, WM_PAINT, OnPaint);
        HANDLE_MSG(hwnd, WM_COMMAND, OnCommand);
    }
    return DefWindowProc(hwnd, message, wParam, lParam);

    
    
    //switch (message)
    //{
    //case WM_COMMAND:
    //    {
    //        int wmId = LOWORD(wParam);
    //        // Parse the menu selections:
    //        switch (wmId)
    //        {
    //        case IDM_ABOUT:
    //            DialogBox(hInst, MAKEINTRESOURCE(IDD_ABOUTBOX), hWnd, About);
    //            break;
    //        //case IDM_EXIT:
    //        //    DestroyWindow(hWnd);
    //        //    break;
    //        default:
    //            return DefWindowProc(hWnd, message, wParam, lParam);
    //        }
    //    }
    //    break;
    //case WM_PAINT:
    //    {
    //        PAINTSTRUCT ps;
    //        HDC hdc = BeginPaint(hWnd, &ps);
    //        // TODO: Add any drawing code that uses hdc here...
    //        EndPaint(hWnd, &ps);
    //    }
    //    break;
    //case WM_DESTROY:
    //    PostQuitMessage(0);
    //    break;
    //default:
    //    return DefWindowProc(hWnd, message, wParam, lParam);
    //}
    //return 0;
}

int originalWidth;
int originalHeight;

void OnPaint(HWND hwnd)
{
    PAINTSTRUCT ps;
    HDC dc = BeginPaint(hwnd, &ps);

    if (g_pBitmap.get() != NULL)
    {
        Graphics g(dc);
        
        RECT windowRect;
        GetClientRect(hwnd, &windowRect);
        
        float overallScalingFactor = 1;
        
        if (windowRect.right <= g_pBitmap->GetWidth() || windowRect.bottom <= g_pBitmap->GetHeight())
        {
            float horizontalScalingFactor = (float)windowRect.right / (float)originalWidth;
            float verticalScalingFactor = (float)windowRect.bottom / (float)originalHeight;
            
            overallScalingFactor = horizontalScalingFactor > verticalScalingFactor ? verticalScalingFactor : horizontalScalingFactor;

            //g.ScaleTransform(overallScalingFactor, overallScalingFactor);
            
        }

        int x = (windowRect.right - (int)g_pBitmap->GetWidth() * overallScalingFactor) / 2;
        int y = (windowRect.bottom - (int)g_pBitmap->GetHeight() * overallScalingFactor) / 2;
        long l = 100;
        long z = 200;
        Gdiplus::Rect size;
        size.X = 100;
        size.Y = 100;
        size.Width = 100;
        size.Height = 100;
        


        g.DrawImage(g_pBitmap.get(), size);
    }

    EndPaint(hwnd, &ps);
}


void OnOpenFile(HWND hwnd, UINT codeNotify)
{
    OPENFILENAME ofn;
    ZeroMemory(&ofn, sizeof(ofn));

    TCHAR fileName[MAX_PATH + 1] = _T("");

    ofn.lStructSize = sizeof(ofn);
    ofn.hwndOwner = hwnd;
    ofn.hInstance = g_hInstance;
    ofn.lpstrFile = fileName;
    ofn.nMaxFile = MAX_PATH;
    ofn.lpstrFilter =
        _T("Images (BMP, PNG, JPG, TIFF)\0*.bmp;*.png;*.jpg;*.tif\0")
        _T("All files\0*.*\0")
        _T("\0");

    if (GetOpenFileName(&ofn))
    {
        Image img(ofn.lpstrFile);

        if (img.GetLastStatus() == Ok)
        {

            g_pBitmap = auto_ptr<Bitmap>(
                new Bitmap(img.GetWidth(), img.GetHeight(), PixelFormat32bppARGB)
                );
            originalHeight = img.GetWidth();
            originalWidth = img.GetHeight();
            Graphics g(g_pBitmap.get());
            g.DrawImage(&img, 0, 0);

            InvalidateRect(hwnd, NULL, TRUE);
        }
    }
}

void OnDestroy(HWND /*hWnd*/)
{
	PostQuitMessage(0);
}

void OnExit(HWND hwnd, UINT codeNotify)
{
    DestroyWindow(hwnd);
}

void OnCommand(HWND hwnd, int id, HWND hwndCtl, UINT codeNotify)
{
    switch (id)
    {
    case ID_FILE_OPEN:
        OnOpenFile(hwnd, codeNotify);
        break;
    }
}

