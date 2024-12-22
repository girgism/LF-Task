import { HttpInterceptorFn } from '@angular/common/http';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authToken = localStorage.getItem('ACCESS_TOKEN');
  const apiBaseUrl = 'https://localhost:7195';
  const isTargetedRequest = (url: string): boolean => url.includes('/api');

  const authReq = req.clone({
    url: isTargetedRequest(req.url) ? `${apiBaseUrl}${req.url}` : req.url,
    setHeaders: {
      Authorization: `Bearer ${authToken}`,
    },
  });
  return next(authReq);
};
