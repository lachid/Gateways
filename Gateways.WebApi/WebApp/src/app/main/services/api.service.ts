import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';

import { MessageService } from 'primeng-lts/api';

import { Observable } from 'rxjs';

@Injectable()
export abstract class ApiService {
    protected abstract url: string;

    constructor(private messageService: MessageService) { }

    protected handleRequest<TResult>(request: Observable<TResult>, showErrorMessage = true): Promise<TResult> {
        const promise = request.toPromise();
        return promise
            .then((response: TResult) => response)
            .catch((errorResponse: HttpErrorResponse) => {
                if (showErrorMessage) {
                    this.handleError(errorResponse);
                }

                return Promise.reject(errorResponse);
            });
    }

    protected handleError(errorResponse: HttpErrorResponse): void {
        const errors: string[] = errorResponse.error?.errors;
        const detail = (!!errors?.length) ? errors.join(' ') : 'Server error';
        this.messageService.add({ severity: 'error', summary:'', detail });
    }
}