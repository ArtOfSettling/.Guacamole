﻿using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using WellFired.Guacamole.Exceptions;

namespace WellFired.Guacamole.Unity.Editor
{
	[Serializable]
	public class Application : IApplication
	{
		public IWindow MainWindow => _mainWindow;
		[SerializeField] private GuacamoleWindow _mainWindow;
		public bool IsRunning => _mainWindow != null;

		public IApplication Launch(InitializationContext initializationContext, Type persistantType = null)
		{
			if (initializationContext == null)
				throw new InitializationContextNull();

			if (persistantType != null)
				ConfigurePersistentData(initializationContext, persistantType);

			_mainWindow = GuacamoleWindowLauncher.LaunchWindow(initializationContext.MainContentType);
			_mainWindow.Launch(initializationContext);
			
			return this;
		}

		private static void ConfigurePersistentData(InitializationContext initializationContext, Type persistantType)
		{
			var assetPath = $"Assets/GuacamoleApplication/Editor/{initializationContext.ApplicationName}/data.asset";
			var persistantData = AssetDatabase.LoadAssetAtPath(assetPath, persistantType);

			if (persistantData == null)
			{
				persistantData = ScriptableObject.CreateInstance(persistantType);
				Directory.CreateDirectory($"{UnityEngine.Application.dataPath}/GuacamoleApplication/Editor/{initializationContext.ApplicationName}/data.asset");
				AssetDatabase.DeleteAsset(assetPath);
				AssetDatabase.CreateAsset(persistantData, assetPath);
				EditorUtility.SetDirty(persistantData);
				AssetDatabase.SaveAssets();
			}

			initializationContext.PersistantData = persistantData as ScriptableObject;
		}

		public void Teardown()
		{
			_mainWindow.CloseAfterNextUpdate = true;
		}

		public void Update()
		{
			_mainWindow.Repaint();
		}
	}
}