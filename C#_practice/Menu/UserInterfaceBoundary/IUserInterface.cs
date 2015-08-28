using System;

namespace UserInterfaceBoundary
{
    public interface IUserInterface
    {
         void Show(Handler handler, UIData data);
    }
}