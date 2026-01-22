import { describe, it, expect, vi } from 'vitest';
import { renderWithProviders, screen } from '@/test/test-utils';
import ProductDetailPage from '@/presentation/features/product/pages/ProductDetailPage';

// Mock useParams and useProduct hook
vi.mock('react-router-dom', async () => {
  const actual = await vi.importActual('react-router-dom');
  return {
    ...actual,
    useParams: () => ({ id: '1' }),
  };
});

vi.mock('@/presentation/features/product/hooks/useProducts', () => ({
  useProduct: () => ({
    data: undefined,
    isLoading: false,
    isError: true,
  }),
}));

describe('ProductDetailPage', () => {
  it('shows error UI when product is not found', () => {
    renderWithProviders(<ProductDetailPage />);

    expect(screen.getByRole('alert')).toBeInTheDocument();
    expect(screen.getByText(/product not found/i)).toBeInTheDocument();
    expect(
      screen.getByRole('button', { name: /continue shopping/i })
    ).toBeInTheDocument();
  });
});


