import { renderWithProviders, screen } from '@/test/test-utils';
import { describe, it, expect } from 'vitest';
import Card from '@/presentation/features/product/components/ProductCard';
import type { Product } from '@core/domain/entities/Product';

const mockProduct: Product = {
    id: 1,
    title: 'Test Product',
    price: 99.99,
    description: 'Test description',
    category: 'electronics',
    image: 'https://via.placeholder.com/150',
    rating: {
        rate: 4.5,
        count: 100,
    },
};

describe('Card Component', () => {
    it('renders product information correctly', () => {
        renderWithProviders(<Card product={mockProduct} />);

        expect(screen.getByText('Test Product')).toBeInTheDocument();
        expect(screen.getByText(/99.99/)).toBeInTheDocument();
        expect(screen.getByText('electronics')).toBeInTheDocument();
    });

    it('displays product image', () => {
        renderWithProviders(<Card product={mockProduct} />);

        const image = screen.getByAltText('Test Product') as HTMLImageElement;
        expect(image).toBeInTheDocument();
        expect(image.src).toContain('placeholder');
    });

    it('shows rating badge', () => {
        renderWithProviders(<Card product={mockProduct} />);

        expect(screen.getByText('⭐ 4.5')).toBeInTheDocument();
    });

    it('has add to cart button', () => {
        renderWithProviders(<Card product={mockProduct} />);

        const addButton = screen.getByRole('button', { name: /sepete ekle/i });
        expect(addButton).toBeInTheDocument();
    });
});
