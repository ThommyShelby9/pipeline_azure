import { describe, it, expect, vi } from 'vitest';
import { renderWithProviders, screen } from '@/test/test-utils';
import HomePage from '@/presentation/features/product/pages/HomePage';

vi.mock('@/presentation/features/product/hooks/useProducts', () => ({
  useProducts: () => ({
    data: [],
    isLoading: false,
    isError: false,
  }),
}));

describe('HomePage', () => {
  it('renders empty products state with i18n texts', () => {
    renderWithProviders(<HomePage />);

    expect(screen.getByText(/product not found/i)).toBeInTheDocument();
  });
});


