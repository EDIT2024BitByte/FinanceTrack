export const environment = {
  webApiBudgetUrl: typeof window !== 'undefined' ? window.settings.webApiBudgetUrl : '',
  webApiUserUrl: typeof window !== 'undefined' ? window.settings.webApiUserUrl : '',
  webApiCashflowUrl: typeof window !== 'undefined' ? window.settings.webApiCashflowUrl : '',
};
