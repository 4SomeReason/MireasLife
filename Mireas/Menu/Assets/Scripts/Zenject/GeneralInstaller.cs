using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GeneralInstaller : MonoInstaller
{
    [SerializeField] private DialogInstaller dialogueInstaller;
    public override void InstallBindings()
    {
        Container.Bind<DialogInstaller>().FromInstance(dialogueInstaller).AsSingle();
    }
}
