using UnityEngine;
namespace Symbiotic.Actor
{
    public interface IActorInterface
    {
        GameObject GameObject { get; set; }
        bool Enabled { get; }
    }
}