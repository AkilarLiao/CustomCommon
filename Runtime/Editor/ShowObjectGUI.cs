/// <summary>
/// Author: AkilarLiao
/// Date: 2023/06/17
/// Desc:
/// </summary>

using UnityEngine;
using UnityEditor;

namespace CustomCommon
{
	public class ShowObjectGUI
	{
		protected void DestorySelfEditor()
		{
			if (m_selfEditor)
			{
				SafeDestroy(m_selfEditor);
				m_selfEditor = null;
			}
		}

		public virtual void UpdateObjectGUI(Object targetObject, bool isDrawHead = true)
		{   
			if (m_targetObject != targetObject)
				DestorySelfEditor();

			if (!targetObject)
				return;

			m_targetObject = targetObject;

			if(!m_selfEditor)
				m_selfEditor = Editor.CreateEditor(targetObject);
			Debug.Assert(m_selfEditor);

			OupdateGUI(isDrawHead);
		}
		protected virtual void OupdateGUI(bool isDrawHead)
		{
			if (isDrawHead)
			{
				m_selfEditor.DrawHeader();
				EditorGUI.indentLevel++;
			}
			m_selfEditor.OnInspectorGUI();
		}

		protected static void SafeDestroy<T>(T obj,
			bool ignoreDataLoss = false) where T : Object
		{
			if (obj == null)
				return;
			if (Application.isEditor)
				Object.DestroyImmediate(obj, ignoreDataLoss);
			else
				Object.Destroy(obj);

			obj = null;
		}

		protected Editor m_selfEditor = null;
		protected Object m_targetObject = null;
	}
}