export interface Language {
    languageId: string;
    code: string;
    name: string;
}

export interface LanguageDto {
    id: string;
    code: string;
    name: string;
}

export interface Key {
    keyId: string;
    name: string;
}

export interface Translation {
    keyId: string;
    languageId: string;
    value: string | null;
}

export interface TableRow {
    key: Key;
    translations: Translation[];
}

export interface PaginatedTable {
    languages: Language[];
    rows: {
        items: TableRow[];
        totalCount: number;
        page: number;
        pageSize: number;
        totalPages: number;
        hasPreviousPage: boolean;
        hasNextPage: boolean;
    };
}

export interface Project {
    id: string;
    name: {
        value: string;
    };
    createdAt: string;
    updatedAt: string;
    keys: Key[];
}

export interface UpdateProjectDto {
    name: string;
}

export type Result<T> = { data: T | null; error: string | null };