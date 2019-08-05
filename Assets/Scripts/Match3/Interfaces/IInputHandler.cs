using UnityEngine;

namespace Match3.Interfaces
{
    public interface IInputHandler
    {
        bool DidClickHappen();
        GameObject GetClickedObject();

    }
}