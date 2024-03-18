import { FaceTecCustomization } from 'sdk/FaceTecCustomization';

export const Config = (function () {
  // -------------------------------------
  // REQUIRED
  // Available at https://dev.facetec.com/#/account
  // NOTE: This field is auto-populated by the FaceTec SDK Configuration Wizard.
  const DeviceKeyIdentifier = 'duiPANe0qZkOZRXcLfeH6d8iKUULUvgg';

  // -------------------------------------
  // REQUIRED
  // The URL to call to process FaceTec SDK Sessions.
  // In Production, you likely will handle network requests elsewhere and without the use of this variable.
  // See https://dev.facetec.com/#/security-best-practices?link=facetec-server-rest-endpoint-security for more information.
  // NOTE: This field is auto-populated by the FaceTec SDK Configuration Wizard.
  const BaseURL = 'https://api.facetec.com/api/v3.1/biometrics';

  // -------------------------------------
  // REQUIRED
  // The FaceScan Encryption Key you define for your application.
  // Please see https://dev.facetec.com/#/licensing-and-encryption-keys for more information.
  // NOTE: This field is auto-populated by the FaceTec SDK Configuration Wizard.
  const PublicFaceScanEncryptionKey =
    '-----BEGIN PUBLIC KEY-----\n' +
    'MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA5PxZ3DLj+zP6T6HFgzzk\n' +
    'M77LdzP3fojBoLasw7EfzvLMnJNUlyRb5m8e5QyyJxI+wRjsALHvFgLzGwxM8ehz\n' +
    'DqqBZed+f4w33GgQXFZOS4AOvyPbALgCYoLehigLAbbCNTkeY5RDcmmSI/sbp+s6\n' +
    'mAiAKKvCdIqe17bltZ/rfEoL3gPKEfLXeN549LTj3XBp0hvG4loQ6eC1E1tRzSkf\n' +
    'GJD4GIVvR+j12gXAaftj3ahfYxioBH7F7HQxzmWkwDyn3bqU54eaiB7f0ftsPpWM\n' +
    'ceUaqkL2DZUvgN0efEJjnWy5y1/Gkq5GGWCROI9XG/SwXJ30BbVUehTbVcD70+ZF\n' +
    '8QIDAQAB\n' +
    '-----END PUBLIC KEY-----';

  const wasSDKConfiguredWithConfigWizard = true;
  let currentCustomization: FaceTecCustomization;

  function retrieveConfigurationWizardCustomization(
    FaceTecSDK,
    assetsPath: string,
  ): FaceTecCustomization {
    // https://dev.facetec.com/ui-customization#customizations-all-common-customizations

    const outerBackgroundColor = '#444444b0';
    const white = '#f5f5f5';
    const black = '#424242';
    const secondaryColor = '#2b3ea1';
    const secondaryDark = '#162776';
    const primaryColor = '#E15100';
    const primaryLightColor = '#ff6518';
    const disabledBackground = '#DEE0E1';
    const disabledText = '#9E9E9E';
    const borderRadius = '12px';
    const boxShadow = '0px 8px 6px rgba(0, 0, 0, 0.15);';
    const font = 'Inter';

    // Set a default customization
    const theme: FaceTecCustomization = new FaceTecSDK.FaceTecCustomization();

    // 1. Set Cancel Customization
    theme.cancelButtonCustomization.customImage = `${assetsPath}/FaceTec_cancel.png`;
    theme.cancelButtonCustomization.location =
      FaceTecSDK.FaceTecCancelButtonLocation.TopRight;

    // 2. Set Frame Customization
    theme.frameCustomization.borderCornerRadius = borderRadius;
    theme.frameCustomization.backgroundColor = white;
    theme.frameCustomization.borderColor = white;
    theme.frameCustomization.borderWidth = '0px';
    theme.frameCustomization.shadow = boxShadow;

    // 3. Set Security Watermark Customization
    const watermark = FaceTecSDK.FaceTecSecurityWatermarkImage.FaceTec;
    theme.securityWatermarkCustomization.setSecurityWatermarkImage(watermark);

    // 4. Set Overlay Customization
    theme.overlayCustomization.brandingImage = `${assetsPath}/FaceTec_your_app_logo.png`;
    theme.overlayCustomization.backgroundColor = outerBackgroundColor;

    // 5. Set Feedback Customization
    theme.feedbackCustomization.backgroundColor = primaryColor;
    theme.feedbackCustomization.textColor = white;
    theme.feedbackCustomization.shadow = boxShadow;
    theme.feedbackCustomization.cornerRadius = '8px';
    theme.feedbackCustomization.textFont = font;

    // 6. Set Oval Customization
    theme.ovalCustomization.strokeColor = primaryColor;
    theme.ovalCustomization.progressColor1 = primaryColor;
    theme.ovalCustomization.progressColor2 = primaryColor;

    // Set Guidance Customization
    theme.guidanceCustomization.backgroundColors = white;
    theme.guidanceCustomization.foregroundColor = secondaryDark;
    theme.guidanceCustomization.buttonBackgroundNormalColor = primaryLightColor;
    theme.guidanceCustomization.buttonBackgroundDisabledColor =
      disabledBackground;
    theme.guidanceCustomization.buttonBackgroundHighlightColor =
      primaryLightColor;
    theme.guidanceCustomization.buttonTextNormalColor = white;
    theme.guidanceCustomization.buttonTextDisabledColor = disabledText;
    theme.guidanceCustomization.buttonTextHighlightColor = white;
    theme.guidanceCustomization.headerFont = font;
    theme.guidanceCustomization.subtextFont = font;
    theme.guidanceCustomization.buttonFont = font;

    theme.guidanceCustomization.retryScreenImageBorderColor = primaryColor;
    theme.guidanceCustomization.retryScreenImageBorderWidth = '2px';
    theme.guidanceCustomization.retryScreenOvalStrokeColor = primaryColor;
    theme.guidanceCustomization.retryScreenSubtextTextColor = black;

    theme.guidanceCustomization.readyScreenHeaderFont = font;
    theme.guidanceCustomization.readyScreenSubtextTextColor = black;
    theme.guidanceCustomization.readyScreenSubtextFont = font;

    // Set Result Screen Customization
    theme.resultScreenCustomization.backgroundColors = white;
    theme.resultScreenCustomization.foregroundColor = secondaryColor;
    theme.resultScreenCustomization.activityIndicatorColor = primaryLightColor;
    theme.resultScreenCustomization.resultAnimationBackgroundColor =
      primaryLightColor;
    theme.resultScreenCustomization.resultAnimationForegroundColor = white;
    theme.resultScreenCustomization.uploadProgressFillColor = primaryLightColor;
    theme.resultScreenCustomization.messageFont = font;

    // Set ID Scan Customization
    theme.idScanCustomization.selectionScreenBackgroundColors = white;
    theme.idScanCustomization.selectionScreenForegroundColor = secondaryColor;
    theme.idScanCustomization.reviewScreenBackgroundColors = white;
    theme.idScanCustomization.reviewScreenForegroundColor = white;
    theme.idScanCustomization.reviewScreenTextBackgroundColor =
      primaryLightColor;
    theme.idScanCustomization.captureScreenForegroundColor = white;
    theme.idScanCustomization.captureScreenTextBackgroundColor =
      primaryLightColor;
    theme.idScanCustomization.buttonBackgroundNormalColor = primaryLightColor;
    theme.idScanCustomization.buttonBackgroundDisabledColor =
      disabledBackground;
    theme.idScanCustomization.buttonBackgroundHighlightColor =
      primaryLightColor;
    theme.idScanCustomization.buttonTextNormalColor = white;
    theme.idScanCustomization.buttonTextDisabledColor = disabledText;
    theme.idScanCustomization.buttonTextHighlightColor = white;
    theme.idScanCustomization.captureScreenBackgroundColor = white;
    theme.idScanCustomization.captureFrameStrokeColor = white;

    // Set Initial Loading Customization
    theme.initialLoadingAnimationCustomization.backgroundColor = white;
    theme.initialLoadingAnimationCustomization.foregroundColor =
      primaryLightColor;

    theme.vocalGuidanceCustomization.mode = 2;

    this.currentCustomization = theme;
    return theme;
  }

  return {
    wasSDKConfiguredWithConfigWizard,
    DeviceKeyIdentifier,
    BaseURL,
    PublicFaceScanEncryptionKey,
    currentCustomization,
    retrieveConfigurationWizardCustomization,
  };
})();
