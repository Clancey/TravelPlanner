// 
//  Copyright 2012  Xamarin Inc  (http://www.xamarin.com)
// 
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class StateManager
{
	private static Dictionary<string,object> currentState;
	private static object locker = new object();
	private static string stateFile = Path.Combine (System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal), "state");
	
	public static object GetObject(string key)
	{
		lock(locker)
		{
			loadState();
			if(!currentState.ContainsKey(key))
				return null;
			return currentState[key];
		}
	}
	
	public static void SetObject(string key,object value)
	{
		lock(locker)
		{
			loadState();
			
			if(currentState.ContainsKey(key))
				currentState[key] = value;
			else
				currentState.Add(key,value);
		}
	}
	
	private static void loadState()
	{
		if(currentState != null)
			return;
		if(!File.Exists(stateFile))
		{
			currentState = new Dictionary<string, object>();
			return;
		}
		
		var formatter = new BinaryFormatter();
		using(var stream = new FileStream(stateFile,FileMode.Open, FileAccess.Read, FileShare.Read)){
			currentState = (Dictionary<string,object>) formatter.Deserialize(stream);
			stream.Close();
		}
	}
	
	private static void saveState()
	{
		var formatter = new BinaryFormatter();
		using(var stream = new FileStream(stateFile,FileMode.Create,FileAccess.Write, FileShare.None))
		{
			formatter.Serialize(stream, currentState);
			stream.Close();
		}
	}
	public static void Synchronize()
	{
		lock(locker)
		{
			saveState();
		}
	}
}