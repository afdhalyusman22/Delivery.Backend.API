using Backend.API.Error;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Text;

namespace Backend.API
{

    public class ApiResult
    {
        public string status { get; set; }
        public string error { get; set; }
        public string detail { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }

    public class Others
    {
        public static string GenerateHash(string password)
        {
            var saltConf = Settings.AppSettingValue("AppSettings", "SaltHash", "DarkEngine");


            byte[] salt = Encoding.ASCII.GetBytes(saltConf);
            var PasswordHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
               password: saltConf + "." + password + ".Darkengine.NETCore",
               salt: salt,
               prf: KeyDerivationPrf.HMACSHA512,
               iterationCount: 10000,
               numBytesRequested: 128));

            return PasswordHash;
        }
    }

    public class Requests
    {
        internal static IActionResult Response(ControllerBase Controller, ApiStatus statusCode)
        {
            return Response(Controller, statusCode, null, "");
        }
        internal static IActionResult ResponseToken(ControllerBase Controller, ApiStatus statusCode, string token)
        {
            return Response(Controller, statusCode, new { token = token }, "");
        }
        internal static IActionResult DataTableResponse(ControllerBase Controller, ApiStatus statusCode, int _recordsTotal, object dataValue)
        {
            var ResultDataValue = new
            {
                draw = 0,
                recordsFiltered = _recordsTotal,
                recordsTotal = _recordsTotal,
                data = dataValue
            };

            return Controller.StatusCode(statusCode.StatusCode, ResultDataValue);
        }

        internal static IActionResult Response(ControllerBase Controller, ApiStatus statusCode, object dataValue, string msg)
        {
            var e = new ApiStatus(500);

            var _ = new
            {
                status = e.StatusCode,
                error = true,
                detail = "",
                message = e.StatusDescription,
                data = dataValue

            };

            if (statusCode.StatusCode != 200)
            {
                _ = new
                {
                    status = statusCode.StatusCode,
                    error = true,
                    detail = msg,
                    message = statusCode.StatusDescription,
                    data = dataValue
                };
            }
            else
            {
                _ = new
                {
                    status = statusCode.StatusCode,
                    error = false,
                    detail = msg,
                    message = statusCode.StatusDescription,
                    data = dataValue

                };
            }


            return Controller.StatusCode(statusCode.StatusCode, _);
        }

    }

    public class Settings
    {
        private static IDictionary env;

        internal static string AppSettingValue(string name, string key, string envName = "")
        {
            string _envName;

            if (Environment.GetEnvironmentVariables() != null)
            {
                env = Environment.GetEnvironmentVariables();
            }


            if (env["ASPNETCORE_ENVIRONMENT"] == null)
            {
                _envName = envName;
            }
            else
            {
                _envName = "." + env["ASPNETCORE_ENVIRONMENT"].ToString().ToLower();

            }


            string projectPath = AppDomain.CurrentDomain.BaseDirectory.Split(new String[] { @"bin\" }, StringSplitOptions.None)[0];
            try
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(projectPath)
                .AddJsonFile($"appsettings{_envName}.json")
                .Build();

                if (configuration.GetSection(name)[key] == null)
                {
                    return envName;
                }
                else
                {
                    return configuration.GetSection(name)[key];

                }

            }
            catch (Exception)
            {
                return null;
            }

        }

    }
}
