﻿using System.Collections.Generic;
using ClassLibrary1.Model;

namespace ClassLibrary1.Mappers
{
    internal class SettingsMapper
    {
        internal static Settings Map(IDictionary<string, object> settings)
        {
            if (settings == null)
                return null;

            return new Settings(ParameterMapperUtils.Map<string>(settings, "requestTokenUri"), ParameterMapperUtils.Map<string>(settings, "authorizationUri"),
                ParameterMapperUtils.Map<string>(settings, "tokenCredentialsUri"), 
                StringEnumerationMapper.Map(ParameterMapperUtils.Map<object[]>(settings, "signatures")), 
                ParameterMapperUtils.Map<string>(settings, "accessTokenUri"), 
                StringEnumerationMapper.Map(ParameterMapperUtils.Map<object[]>(settings, "authorizationGrants")), 
                ParameterMapperUtils.Map<string>(settings, "flow"), ScopeMapper.Map(ParameterMapperUtils.Map<object[]>(settings, "requestTokenUri")), 
                ParameterMapperUtils.Map<string>(settings, "name"), ParameterMapperUtils.Map<string>(settings, "in"));
        }
    }
}