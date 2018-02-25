using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarmaloopAIMLBotServer.API
{
    public class ResponseModel
    {
        #region Properties
        /// <summary>
        /// Code for the Response
        /// </summary>
        public string Code { get; set; }
        
        /// <summary>
        /// State of conversation
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// UserID of the caller
        /// </summary>
        public string UserID { get; set; }

        #endregion

        #region Constructors
        /// <summary>
        /// Constructor for the Response Model
        /// </summary>
        /// <param name="code"></param>
        /// <param name="state"></param>
        /// <param name="userId"></param>
        public ResponseModel(string code, string state, string userId)
        {
            this.Code = code;
            this.State = state;
            this.UserID = userId;
        }

        #endregion
    }

    #region Support Constants
    /// <summary>
    /// Describes the state of conversation.
    /// </summary>
    public struct StateType
    {
        public const string NORMAL = "NORMAL";
        public const string CONFUSED = "CONFUSED";
        public const string REDIRECT = "REDIRECT";
        public const string UNHANDLED = "UNHANDLED";
    }

    /// <summary>
    /// Describes the Codes that can be returned
    /// </summary>
    public struct CodeType
    {
        public const string SUCCESS = "SUCCESS";
        public const string ERROR = "ERROR"; 
    }

    public struct AuxilliaryUserFunctions
    {
        public const string NEWUSER = "NEW";
    }

    #endregion
}
