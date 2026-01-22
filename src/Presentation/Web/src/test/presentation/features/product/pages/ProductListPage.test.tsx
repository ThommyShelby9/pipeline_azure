import { describe, it, expect, vi } from 'vitest';
import { renderWithProviders, screen } from '@/test/test-utils';
import ProductListPage from '@/presentation/features/product/pages/ProductListPage';

vi.mock('@/presentation/features/product/hooks/useProducts', () => ({
  useProducts: () => ({
    data: [],
    isLoading: false,
    isError: false,
    error: null,
  }),
}));

describe('ProductListPage', () => {
  it('renders header with i18n title', () => {
    renderWithProviders(<ProductListPage />);

    expect(screen.getByRole('heading', { name: /products/i })).toBeInTheDocument();
  });
});


