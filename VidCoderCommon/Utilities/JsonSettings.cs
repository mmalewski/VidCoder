﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using VidCoderCommon.JsonConverters;
using VidCoderCommon.Model;

namespace VidCoderCommon.Utilities
{
	public static class JsonSettings
	{
		public static void SetDefaultSerializationSettings()
		{
			JsonConvert.DefaultSettings = () => new JsonSerializerSettings
			{
				Formatting = Formatting.Indented,
				Converters = new List<JsonConverter>
				{
					new SourceTypeConverter(),
					new StringEnumConverter(),
				},
				ContractResolver = new VidCoderContractResolver()
			};
		}

		public static JsonSerializerSettings HandBrakeJsonSerializerSettings
		{
			get
			{
				return new JsonSerializerSettings
				{
					NullValueHandling = NullValueHandling.Ignore,
				};
			}
		}
	}
}
