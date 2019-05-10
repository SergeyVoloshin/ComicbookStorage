export interface ValidationErrors {
    [name: string]: string[],
}

export interface ErrorResponse {
    errors: ValidationErrors,
}