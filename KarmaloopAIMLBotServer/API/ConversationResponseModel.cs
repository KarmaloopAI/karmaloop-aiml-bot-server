using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarmaloopAIMLBotServer.API
{
    /// <summary>
    /// Sub class of the ResponseModel to handle conversation responses. These can be different from System responses.
    /// </summary>
    public class ConversationResponseModel: ResponseModel
    {
        #region Properties
        /// <summary>
        /// Contains the response text string
        /// </summary>
        public string ResponseText { get; set; }

        #endregion

        #region Constructors
        /// <summary>
        /// Constructor for ConversationResponse Model
        /// </summary>
        /// <param name="responseText"></param>
        /// <param name="userId"></param>
        public ConversationResponseModel(string responseText, string userId)
            :base(CodeType.SUCCESS, StateType.NORMAL, userId)
        {
            this.ResponseText = responseText;
        }
        #endregion
    }
}
