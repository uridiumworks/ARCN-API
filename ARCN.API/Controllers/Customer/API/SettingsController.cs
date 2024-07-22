using NovaBank.API.Filters;
using NovaBank.Application.DataModels.Beneficiary;
using NovaBank.Application.DataModels.ChangePasswordRequest;
using NovaBank.Application.DataModels.TransactionPinReset;
using NovaBank.Application.DataModels.UserProfile;
using System.Threading;

namespace NovaBank.API.Controllers.Customer.API
{

    /// <summary>
    /// Settings Controller
    /// </summary>
    [Route("customer/api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingsService settingsService;
        private readonly ITransferService transferService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="settingsService"></param>
        /// <param name="transferService"></param>
        public SettingsController(ISettingsService settingsService, ITransferService transferService)
        {
            this.settingsService = settingsService;
            this.transferService = transferService;
        }

        /// <summary>
        /// Change Password
        /// </summary>
        /// <param name="changePasswordRequest"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomerCompletedOnboardingFilter))]
        [HttpPost("ChangePassword")]
        [Produces("application/json", Type = typeof(ResponseModel<string>))]

        public async Task<ActionResult> ChangePassword([FromBody] CurrentPasswordModel changePasswordRequest)
        {
            var resp = await settingsService.ChangePasswordAsync(changePasswordRequest);
            if (resp.Success)
            {
                return Ok(resp);
            }
            else
            {
                return BadRequest(resp);
            }
        }


        /// <summary>
        /// Confirm Change Password
        /// </summary>
        /// <param name="changePasswordRequest"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomerCompletedOnboardingFilter))]
        [HttpPost("ConfirmChangePassword")]
        [Produces("application/json", Type = typeof(ResponseModel<string>))]
        public async Task<ActionResult> ConfirmChangePassword([FromBody] ChangePasswordRequest changePasswordRequest)
        {
            var response = await settingsService.ConfirmChangePasswordAsync(changePasswordRequest);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }


        /// <summary>
        /// Update User Profile
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomerCompletedOnboardingFilter))]
        [HttpPost("UpdateUserProfile")]
        [Produces("application/json", Type = typeof(ResponseModel<string>))]
        public async ValueTask<ActionResult> UpdateUserProfile([FromBody] UpdateUserProfileDataModel model)
        {

            var updateUserProfile = await settingsService.UpdateUserProfileAsync(model);

            return Ok(updateUserProfile);

        }


        /// <summary>
        /// Request Update
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        [HttpPost("RequestUpdate")]
        [Produces("application/json", Type = typeof(ResponseModel<string>))]
        public async ValueTask<ActionResult> RequestUpdate([FromBody] RequestUpdateDataModel model)
        {
            var result = await settingsService.RequestUpdateProfileAsync(model);

            return Ok(result);

        }

        /// <summary>
        /// Initiate Forgot Pin
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomerCompletedOnboardingFilter))]
        [HttpPost("InitiateForgotPin")]
        [Produces("application/json", Type = typeof(ResponseModel<string>))]
        public async Task<ActionResult> InitiateForgotPin(ForgotPinSecurityQuestionModel model)
        {
            var response = await settingsService.InitiateForgotPin(model);
            if (response.Success == true)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }


        /// <summary>
        /// initiate pin reset
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomerCompletedOnboardingFilter))]
        [HttpPost("InitiatePinReset")]
        [Produces("application/json", Type = typeof(ResponseModel<string>))]
        public async Task<ActionResult> InitiatePinReset(InitiatePinResetRequest request)
        {
            var result = await settingsService.InitiatePinReset(request);
            if (result.Success == true)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        /// <summary>
        /// Confirm Pin Reset
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomerCompletedOnboardingFilter))]
        [HttpPost("ConfirmPinReset")]
        [Produces("application/json", Type = typeof(ResponseModel<ApplicationUser>))]
        public async Task<ActionResult> ConfirmPinReset(SetNewPinModel request)
        {
            var result = await settingsService.SetNewPin(request);
            if (result.Success == true)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        /// <summary>
        /// Resend OTP
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        [HttpPost("ResendOTP")]
        [Produces("application/json", Type = typeof(ResponseModel<string>))]
        public async ValueTask<ActionResult> ResendOTP(InitiatePinResetRequest request)
        {
            var result = await settingsService.ResendOTP(request);
            return Ok(result);
        }


        /// <summary>
        /// Save Beneficiary
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomerCompletedOnboardingFilter))]
        [HttpPost("SaveBeneficiary")]
        [Produces("application/json", Type = typeof(ResponseModel<TransferBeneficiary>))]
        public async ValueTask<ActionResult> SaveBeneficiary([FromBody] BeneficiaryDataModel request)
        {
            var response = await transferService.SaveBeneficiaryAsync(request, CancellationToken.None);
            if (response.Success == true)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        /// <summary>
        /// Delete Beneficiary
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomerCompletedOnboardingFilter))]
        [HttpDelete("DeleteBeneficiary/{key}")]
        [Produces("application/json", Type = typeof(ResponseModel<TransferBeneficiary>))]
        public async ValueTask<ActionResult> DeleteBeneficiary(Guid key)
        {
            var response = await transferService.DeleteBeneficiaryAsync(key);
            if (response.Success == true)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }



        /// <summary>
        /// Register Fingerprint
        /// </summary>
        /// <param name="registerFingerprint"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomerCompletedOnboardingFilter))]
        [HttpPost("biometric/register")]
        [Produces("application/json", Type = typeof(ResponseModel<string>))]
        public async Task<ActionResult> RegisterFingerprint([FromBody] RegisterFingerprintDataModel registerFingerprint)
        {
            var result = await settingsService.RegisterFingerprint(registerFingerprint);
            if (result.Success == true)
                return Ok(result);
            return BadRequest(result);
        }


        /// <summary>
        /// Enable Biometric For Transaction
        /// </summary>
        /// <param name="enableBiometrics"></param>
        /// <returns></returns>
        [ServiceFilter(typeof(CustomerCompletedOnboardingFilter))]
        [HttpPost("biometric/transaction")]
        [Produces("application/json", Type = typeof(ResponseModel<string>))]
        public async Task<ActionResult> EnableBiometricForTransaction([FromBody] EnableBiometricsForTansactionDataModel enableBiometrics)
        {
            var result = await settingsService.EnableBiometricForTransaction(enableBiometrics);
            if (result.Success == true)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
